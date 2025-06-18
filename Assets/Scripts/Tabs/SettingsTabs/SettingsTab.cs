using System.Collections.Generic;
using InputDeviceOverlay;
using Screens;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;

namespace Tabs.SettingsTabs
{
    public class SettingsTab: ScreenTab
    {
        [SerializeField] private List<RowController> _rows;
        [SerializeField] private Button _settingsButton;
        
        private RowController _defaultRow;

        protected override void Awake()
        {
            base.Awake();
            SetDefaultRow();
        }

        private void OnValidate()
        {
            SetDefaultRow();
        }

        private void SetDefaultRow()
        {
            _rows = new List<RowController>(GetComponentsInChildren<RowController>(includeInactive: false));
            if (_rows.Count == 0)
            {
                Debug.LogError($"[SettingsTab] no rows found");
            }
        }

        public void JumpToDefaultRow()
        {
          //  if(!ControllerDetector.Instance.IsGamepadActive) return;
            
            EventSystem.current.SetSelectedGameObject(_rows[0].gameObject);
        }
 
    }
}