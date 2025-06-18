using Tabs.SettingsTabs;
using UnityEngine.EventSystems;

namespace Screens
{
    public class GameSettingsScreen: MenuScreen
    {
        public override void OnCancelClick()
        {
            base.OnCancelClick();
           
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