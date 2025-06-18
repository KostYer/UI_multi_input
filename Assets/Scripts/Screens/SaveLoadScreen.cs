using System.Collections.Generic;
using DG.Tweening;
using SaveLoading;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Screens
{
    public class SaveLoadScreen: MenuScreen
    {
        [SerializeField] private SelectionResolver _selectionResolver;
         
        [SerializeField] private List<SaveSlot> _saveSloats = new();
        
        [Header("Scroll View References")]
        public ScrollRect scrollView;  
        public RectTransform viewport;  
        public RectTransform content;   

        [Header("Scroll Settings")]
        public float scrollDuration = 0.2f; 
        public Ease scrollEase = Ease.OutQuad;  
        public float padding = 60f; 
        
       


        private GameObject lastSelectedGameObject;

        protected override void Awake()
        {
            base.Awake();
            _selectionResolver.OnSelectionChanged += OnSelectionChanged;

            if (_saveSloats.Count == 0)
            {
                Debug.LogError($"[SaveLoadScreen] no save slots provided");
                return;
            }

            foreach (var slot in _saveSloats)
            {
                slot.OnSlotPicked += OnSlotPicked;
            }
        }

        private void OnSelectionChanged(bool isSelected, GameObject selection)
        {
            if (isSelected)
            {
        //        Debug.Log($"[SaveLoadScreen] Selected: {isSelected}, Selection: {selection.name}");
                lastSelectedGameObject = selection;
                ScrollToSelected(lastSelectedGameObject.GetComponent<RectTransform>());
                return;
            }
      //      Debug.Log($"[SaveLoadScreen] Selected: {isSelected}");
          
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
        
        private void OnSlotPicked(SaveSlot slot )
        {
             var tab = (LoadTab) tabs[TabType.LoadSave];
             tab.Init(slot);
             ShowTab(TabType.LoadSave);
             AnimatePopUp_ScaleAndMove();
        }
        
        public void AnimatePopUp_ScaleAndMove()
        {
            float animationDuration = .15f;
            // Set initial state
            tabs[TabType.LoadSave].GetComponent<RectTransform>().localScale = Vector3.zero; // Start from scale 0
            

            Sequence sequence = DOTween.Sequence();

            // Step 1: Fade In
            sequence.Append( tabs[TabType.LoadSave].CanvasGroup.DOFade(1f, animationDuration).SetEase(Ease.OutQuint));

            // Step 2: Scale Up (overlaps with fade for snappier feel)
            sequence.Join( tabs[TabType.LoadSave].GetComponent<RectTransform>().DOScale(1f, animationDuration).SetEase(Ease.OutQuint));  

            // Step 3 (Optional if you want a slight shift during scale up):
            // If you want it to appear *from* the main menu's edge and push out,
            // you would start its anchoredPosition differently and move it.
            // For "pop up right of", starting at scale 0 at the target position and scaling up is often sufficient.
            // If you want a slight initial "push" from the main menu, set its initial X slightly to the left of targetX
            // and add a DOMoveX to the sequence. Example:
            // popUpMenuRectTransform.anchoredPosition = new Vector2(mainMenuRectTransform.anchoredPosition.x + mainMenuRectTransform.rect.width / 2, targetY); // Start at main menu's right edge
            // sequence.Join(popUpMenuRectTransform.DOAnchorPosX(targetX, animationDuration).SetEase(Ease.OutSine)); // Then move to final X
        }

        
    }
}