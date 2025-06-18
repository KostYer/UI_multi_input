using UnityEngine;

namespace InputDeviceOverlay
{
    [RequireComponent(typeof(CanvasGroup))]
    public class OverlayUnit: MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private ControllerType _controllerType;
        
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
        }
    }
}