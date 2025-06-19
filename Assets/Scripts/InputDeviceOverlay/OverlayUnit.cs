using UnityEngine;

namespace InputDeviceOverlay
{
    [RequireComponent(typeof(CanvasGroup))]
    public class OverlayUnit: MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private ControllerType _controllerType;
        [SerializeField] private CanvasGroup _ignorable;
        
        public ControllerType ControllerType => _controllerType;
        public CanvasGroup CanvasGroup => _canvasGroup;

        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Awake()
        {
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            if(_ignorable == null) return;
            _ignorable.ignoreParentGroups = false;
        }

        public void Show()
        {
            if(_ignorable == null) return;
            _ignorable.ignoreParentGroups = true;
            _ignorable.alpha = 1f;
        }

        public void Hide()
        {
            CanvasGroup.alpha = 0f;
            if(_ignorable == null) return;
            _ignorable.ignoreParentGroups = false;
            _ignorable.alpha = 0f;
        }
    }
}