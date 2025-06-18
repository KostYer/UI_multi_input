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
            if (!isSelected) return;
                lastSelectedGameObject = selection;
                ScrollToSelected(lastSelectedGameObject.GetComponent<RectTransform>());
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
            tabs[TabType.LoadSave].GetComponent<RectTransform>().localScale = Vector3.zero;  
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append( tabs[TabType.LoadSave].CanvasGroup.DOFade(1f, animationDuration).SetEase(Ease.OutQuint));
            sequence.Join( tabs[TabType.LoadSave].GetComponent<RectTransform>().DOScale(1f, animationDuration).SetEase(Ease.OutQuint));  
        }
        
        public override void OnCancelClick()
        {
            base.OnCancelClick();
           if(!IsActive) return;
           UIController.Instance.ShowScreen(ScreenType.MainMenu);
        }
    }
}