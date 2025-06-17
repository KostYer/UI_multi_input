using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Screens
{
    public abstract class MenuScreen: MonoBehaviour
    {
        [SerializeField] private CanvasGroupWrapper _canvasGroupWrapper;
        [SerializeField] private List<ScreenTab> _loadTabs = new();
        [SerializeField] private ScreenTab _mainTab;
        
        public TabType MainTabType => _mainTab==null? TabType.NotImplemented : _mainTab.TabType;
        
        protected Dictionary<TabType, ScreenTab> tabs = new();

        protected virtual void Awake()
        {
            if(_loadTabs == null) return;
            if(_loadTabs.Count == 0) return;
        

            for (int i = 0; i < _loadTabs.Count; i++)
            {
                var key = _loadTabs[i].TabType;
                if(tabs.ContainsKey(key)) continue;
                
                tabs.Add(key, _loadTabs[i]);
                _loadTabs[i].OnTabOnen += OnTabOpen;
            }
        }

        protected virtual void OnTabOpen(TabType tabType)
        {
           
        }

        private void OnValidate()
        {
            _canvasGroupWrapper = GetComponent<CanvasGroupWrapper>();
        }
        
        public virtual void Show()
        {
            _canvasGroupWrapper.Show();
            if(_mainTab != null) _mainTab.Show();
        }
        
        public virtual void Show(TabType tabType)
        {
            _canvasGroupWrapper.Show();
            ShowTab(tabType);
        }

        public virtual void Hide()
        {
            _canvasGroupWrapper.Hide();
        }

        public virtual void ShowTab(TabType tabType)
        {
            if (!tabs.ContainsKey(tabType))
            {
                Debug.LogError($"[MenuScreen] no tab found for type {tabType}");
                return;
            }

            foreach (var tp in tabs.Keys)
            {
                if(tp != tabType) tabs[tp].Hide();
            }
            tabs[tabType].Show();
        }

        public void ShowTab(int tabIndex)
        {
            TabType tabType = GetTabByIndex(tabIndex);
            ShowTab(tabType);
        }
        public void ShowMainTab()
        {
            Show(MainTabType);
        }

        public void ShowNotImplementedTab()
        {
            Show(TabType.NotImplemented);
        }
        
        public TabType GetTabByIndex(int index)
        {
            if (index >= 0 && index < _loadTabs.Count)
                return _loadTabs[index].TabType;
            return TabType.None;
        }
    }
}