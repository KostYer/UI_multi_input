using UnityEngine;

namespace Screens
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class ScreenTab: MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TabType _tabType;
        
        public TabType TabType => _tabType;
        public CanvasGroup CanvasGroup => _canvasGroup;
        
        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public virtual void Show()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public virtual void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}