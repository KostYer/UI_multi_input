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
        [SerializeField] private GameObject _secondSelected;
        [SerializeField] private RectTransform _playGameMenu;
        [SerializeField] private RectTransform _mainPanel;

        [SerializeField] private SelectionResolver _selectionResolver;
        
        
        [Header("anim settings")]
        [SerializeField] private float animationDuration = .15f;

        private bool _isAdditionalPanelShowed;

        protected override void Awake()
        {
            base.Awake();
            _playGameMenu.localScale = Vector3.zero;
            tabs[TabType.mainMenuIntro].OnTabOnen += OnMainTabOpen;

        }

        private void OnMainTabOpen(TabType obj)
        {
            _isAdditionalPanelShowed = false;;
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
            sequence.Append(_playGameMenu.GetComponent<CanvasGroup>().DOFade(endAlpha, animationDuration).SetEase(Ease.OutQuint));
            sequence.Join(_playGameMenu.DOScale(endScale, animationDuration).SetEase(Ease.OutQuint));  
        }

        public void OnAdditionalPanel()
        {
            _isAdditionalPanelShowed = !_isAdditionalPanelShowed;
            AnimatePopUp_ScaleAndMove(_isAdditionalPanelShowed);
            if(!_isAdditionalPanelShowed) return;
            EventSystem.current.SetSelectedGameObject(_secondSelected); 
        }
    }
}