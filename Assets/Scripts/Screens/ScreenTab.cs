using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class ScreenTab: MonoBehaviour
    {
        public event Action<TabType> OnTabOnen;
        [SerializeField] protected CanvasGroup _canvasGroup;
        [SerializeField] private TabType _tabType;
       
        [SerializeField] protected InputActionAsset _inputActions;
        [SerializeField] protected GameObject _defaultSelection;
        [SerializeField]  private bool _isActive;
        
        private InputAction _navigateAction;
        
        public GameObject DefaultSelection => _defaultSelection;
        public bool IsActive => _isActive;
        public TabType TabType => _tabType;
        public CanvasGroup CanvasGroup => _canvasGroup;

        protected virtual void Awake()
        {
            _navigateAction =  _inputActions.FindAction("UI/Navigate");
            _navigateAction.performed += OnNavigation;
            _navigateAction.Enable();
        }

        private void OnNavigation(InputAction.CallbackContext obj)
        {
            if (EventSystem.current.currentSelectedGameObject != null) return;
            SelectDefaultSelection();
        }

        private void SelectDefaultSelection()
        {
            if(!_isActive) return;
            if (_defaultSelection == null)
            {
                Debug.LogError($"[ScreenTab] OnNavigation. defaultSelection is null");
            }
           if (EventSystem.current.currentSelectedGameObject == _defaultSelection) return;
           
            
            EventSystem.current.SetSelectedGameObject(_defaultSelection);
        }

        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Show()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _isActive = true;
            OnTabOnen?.Invoke(_tabType);
            SelectDefaultSelection();
        }

        public virtual void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _isActive = false;
        }
    }
}