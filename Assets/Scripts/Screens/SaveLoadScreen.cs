using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Screens
{
    public class SaveLoadScreen: MenuScreen
    {
        [SerializeField] private SelectionController _selectionController;
        
        [Header("Scroll View References")]
        public ScrollRect scrollView;  
        public RectTransform viewport;  
        public RectTransform content;   

        [Header("Scroll Settings")]
        public float scrollDuration = 0.2f; 
        public Ease scrollEase = Ease.OutQuad;  
        public float padding = 60f; 

        private GameObject lastSelectedGameObject;

        private void Awake()
        {
            _selectionController.OnSelectionChanged += OnSelectionChanged;
        }
        private void OnSelectionChanged(bool isSelected, GameObject selection)
        {
            if (isSelected)
            {
                Debug.Log($"[SaveLoadScreen] Selected: {isSelected}, Selection: {selection.name}");
                lastSelectedGameObject = selection;
                ScrollToSelected(lastSelectedGameObject.GetComponent<RectTransform>());
                return;
            }
            Debug.Log($"[SaveLoadScreen] Selected: {isSelected}");
          
        }
        
        private void ScrollToSelected(RectTransform selectedItemRect)
        {
        float selectedItemYInContent = -selectedItemRect.anchoredPosition.y; // Y is negative downwards from top
        // Calculate the top and bottom bounds of the selected item in content space
        float selectedItemTop = selectedItemYInContent - (selectedItemRect.rect.height * (1f - selectedItemRect.pivot.y));
        float selectedItemBottom = selectedItemYInContent + (selectedItemRect.rect.height * selectedItemRect.pivot.y);
        // Get the current scroll position (0 = top, 1 = bottom)
        float currentScrollNormalizedPosition = scrollView.verticalNormalizedPosition;
        // Get the visible area of the viewport in content coordinates
        float viewportHeight = viewport.rect.height;
        float contentHeight = content.rect.height;
        // Visible top and bottom in content coordinates relative to content's top edge
        float visibleTopInContent = (1f - currentScrollNormalizedPosition) * (contentHeight - viewportHeight);
        float visibleBottomInContent = visibleTopInContent + viewportHeight;
        float targetNormalizedPosition = currentScrollNormalizedPosition;
        bool needsScroll = false;
        // Check if the item is above the visible area
        if (selectedItemTop - padding < visibleTopInContent)
        {
            // Calculate the new scroll position to bring the item to the top of the viewport + padding
            float newVisibleTop = selectedItemTop - padding;
            targetNormalizedPosition = 1f - (newVisibleTop / (contentHeight - viewportHeight));
            needsScroll = true;
        }
        // Check if the item is below the visible area or partially off
        else if (selectedItemBottom + padding > visibleBottomInContent)
        {
            // Calculate the new scroll position to bring the item to the bottom of the viewport - padding
            float newVisibleBottom = selectedItemBottom + padding;
            float newVisibleTop = newVisibleBottom - viewportHeight;
            targetNormalizedPosition = 1f - (newVisibleTop / (contentHeight - viewportHeight));
            needsScroll = true;
        }

        // Clamp the target normalized position between 0 and 1
        targetNormalizedPosition = Mathf.Clamp01(targetNormalizedPosition);

        if (needsScroll)
        {
            // Stop any ongoing scroll tween to prevent conflicts
            scrollView.DOKill(true);

            // Animate the scroll position
            scrollView.DOComplete(); // Complete any existing tweens on the scrollView
            scrollView.DOVerticalNormalizedPos(targetNormalizedPosition, scrollDuration).SetEase(scrollEase);
        }
        }
    }
}