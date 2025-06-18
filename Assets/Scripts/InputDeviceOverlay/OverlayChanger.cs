using System;
using DG.Tweening;
using UnityEngine;
using Utils;

namespace InputDeviceOverlay
{
    public class OverlayChanger: MonoBehaviour
    {
        [SerializeField] private CanvasGroupWrapper _xBoxOverlay;
        [SerializeField] private CanvasGroupWrapper _psOverlay;
        
        [SerializeField] private float _fadeDuration = 0.2f;
         private ControllerType _controllerTypeCurrent;
        
        private Tween _currentTween;
        
        private void Start()
        {
            ControllerDetector.Instance.OnControllerChanged += OnOverlayChanged;
            _controllerTypeCurrent = ControllerDetector.Instance.CurrentControlsType;
        }

        private void OnOverlayChanged(ControllerType controllerType)
        {
            Debug.Log($"[OverlayChanger] OnOverlayChanged {controllerType}");
            switch (controllerType)
            {
               
                case ControllerType.XBox:
                    SwitchOverlay(_xBoxOverlay.CanvasGroup, _psOverlay.CanvasGroup);
                    _controllerTypeCurrent = ControllerDetector.Instance.CurrentControlsType;
                    break;
                case ControllerType.PS:
                    SwitchOverlay(_psOverlay.CanvasGroup, _xBoxOverlay.CanvasGroup);
                    _controllerTypeCurrent = ControllerDetector.Instance.CurrentControlsType;
                    break;
                default:
                    HideCurrentOverlay();
                  //  _controllerTypeCurrent = ControllerDetector.Instance.CurrentControlsType;
                    break;
            }
        }
      
        private void SwitchOverlay(CanvasGroup toShow, CanvasGroup toHide)
        {
            _currentTween?.Kill();

            // Start a joined tween sequence
            _currentTween = DOTween.Sequence()
                .Join(toShow.DOFade(1f, _fadeDuration))
                .Join(toHide.DOFade(0f, _fadeDuration))
                .OnComplete(Reset);
        }

        private void HideCurrentOverlay()
        {
            _currentTween?.Kill();
            var toHide = _controllerTypeCurrent == ControllerType.XBox ?_xBoxOverlay : _psOverlay;
            _currentTween = toHide.CanvasGroup.DOFade(0f, _fadeDuration).OnComplete(Reset);
        }

        private void Reset()
        {
            _controllerTypeCurrent = ControllerDetector.Instance.CurrentControlsType;
            _currentTween = null;
        }
    }
}