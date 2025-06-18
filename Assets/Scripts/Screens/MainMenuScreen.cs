using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;
using Sequence = DG.Tweening.Sequence;

namespace Screens
{
    public class MainMenuScreen : MenuScreen
    {
        [SerializeField] private GameObject _firstSelected;
        [SerializeField] private RectTransform _playGameMenu;
        [SerializeField] private RectTransform _mainPanel;

        [SerializeField] private SelectionResolver _selectionResolver;
        
        
        [Header("anim settings")]
        [SerializeField] private float animationDuration = .15f;

        protected override void Awake()
        {
            base.Awake();
            _playGameMenu.localScale = Vector3.zero;
           
        }
 

        private void OnSelectionChanged(bool isSelection, GameObject selected)
        {
            if (isSelection) return;
            Debug.Log($"[MainMenuScreen sssss] OnSelectionChanged {isSelection}");
            
            AnimatePopUp_ScaleAndMove(false);
        }

        protected override void OnTabOpen(TabType tabType)
        {
            base.OnTabOpen(tabType);
            AnimatePopUp_ScaleAndMove(false);
            
        }

        void OnEnable()
        {
            Debug.Log("[MainMenuScreen] OnEnable");
            _selectionResolver.OnSelectionChanged += OnSelectionChanged;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_firstSelected);
        }
        
        public void AnimatePopUp_ScaleAndMove(bool show)
        {
            var endScale = show ? 1f : 0f;
            var endAlpha = show ? 1f : 0f;
            
            if (show)
            {
                _playGameMenu.localScale = Vector3.zero; // Start from scale 0
            }

           




            Sequence sequence = DOTween.Sequence();

            // Step 1: Fade In
            sequence.Append(_playGameMenu.GetComponent<CanvasGroup>().DOFade(endAlpha, animationDuration).SetEase(Ease.OutQuint));

            // Step 2: Scale Up (overlaps with fade for snappier feel)
            sequence.Join(_playGameMenu.DOScale(endScale, animationDuration).SetEase(Ease.OutQuint));  

            // Step 3 (Optional if you want a slight shift during scale up):
            // If you want it to appear *from* the main menu's edge and push out,
            // you would start its anchoredPosition differently and move it.
            // For "pop up right of", starting at scale 0 at the target position and scaling up is often sufficient.
            // If you want a slight initial "push" from the main menu, set its initial X slightly to the left of targetX
            // and add a DOMoveX to the sequence. Example:
          //  _playGameMenu.anchoredPosition = new Vector2(_mainPanel.anchoredPosition.x + _mainPanel.rect.width / 2, -481); // Start at main menu's right edge
          //    sequence.Join(_playGameMenu.DOAnchorPosX(-400, animationDuration).SetEase(Ease.OutSine)); // Then move to final X
        }

      
    }
}