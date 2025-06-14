using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Screens
{
    public class MainMenuScreen : MenuScreen
    {
        [SerializeField] private GameObject _firstSelected;
        [SerializeField] private RectTransform _playGameMenu;

        private void Awake()
        {
            _playGameMenu.localScale = Vector3.zero;  
        }

        void OnEnable()
        {
            Debug.Log("[MainMenuScreen] OnEnable");
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_firstSelected);
        }
        
        
        
        
        
       
        public void AnimatePopUp_ScaleAndMove()
        {
            float animationDuration = .15f;
            // Set initial state
            _playGameMenu.localScale = Vector3.zero; // Start from scale 0
            

            Sequence sequence = DOTween.Sequence();

            // Step 1: Fade In
            sequence.Append(_playGameMenu.GetComponent<CanvasGroup>().DOFade(1f, animationDuration).SetEase(Ease.OutQuint));

            // Step 2: Scale Up (overlaps with fade for snappier feel)
            sequence.Join(_playGameMenu.DOScale(1f, animationDuration).SetEase(Ease.OutQuint));  

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