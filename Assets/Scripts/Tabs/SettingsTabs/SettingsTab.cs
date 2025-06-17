using System.Collections.Generic;
using InputDeviceOverlay;
using Screens;
using UnityEngine;
using UnityEngine.EventSystems;
using Utils;

namespace Tabs.SettingsTabs
{
    public class SettingsTab: ScreenTab
    {
        [SerializeField] private List<RowController> _rows;
        
        private void OnValidate()
        {
            _rows = new List<RowController>(GetComponentsInChildren<RowController>(includeInactive: false));
            if (_rows.Count == 0)
            {
                Debug.LogError($"[SettingsTab] no rows found");
            }
        }

        public override void Show()
        {
            base.Show();
            if(!ControllerDetector.Instance.IsGamepadActive) return;
            
            EventSystem.current.SetSelectedGameObject(_rows[0].gameObject);
        }
    }
}