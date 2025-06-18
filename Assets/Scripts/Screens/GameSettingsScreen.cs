using Tabs.SettingsTabs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Screens
{
    public class GameSettingsScreen: MenuScreen
    {
        [SerializeField] private InputActionAsset _inputActions;
        private InputAction _goBackAction;
        
        
        protected override void Awake()
        {
            _goBackAction = _inputActions.FindAction("UI/Cancel");
            _goBackAction.performed += OnCancelClick;
            _goBackAction.Enable();
            base.Awake();
        }

        private void OnCancelClick(InputAction.CallbackContext obj)
        {
            Debug.Log($"[GameSettingsScreen] OnCancelClick");
            if (EventSystem.current.currentSelectedGameObject == _activeTab.DefaultSelection)
            {
                UIController.Instance.ShowScreen(ScreenType.MainMenu);
                return;
            }
            
            EventSystem.current.SetSelectedGameObject(_activeTab.DefaultSelection);

        }

        public void OnTabClick()
         {
             var tab = (SettingsTab)_activeTab;
             tab.JumpToDefaultRow();
         }
    }
}