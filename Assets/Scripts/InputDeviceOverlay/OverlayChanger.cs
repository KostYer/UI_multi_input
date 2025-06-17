using System;
using UnityEngine;
using Utils;

namespace InputDeviceOverlay
{
    public class OverlayChanger: MonoBehaviour
    {
        [SerializeField] private CanvasGroupWrapper _mouseKeyboardOverlay;
        [SerializeField] private CanvasGroupWrapper _xBoxOverlay;
        [SerializeField] private CanvasGroupWrapper _psOverlay;
        private void Start()
        {
            ControllerDetector.Instance.OnControllerChanged += OnOverlayChanged;
           // ControllerDetector.Instance.ForceUpdateStatus();
        }

        private void OnOverlayChanged(ControllerType controllerType)
        {
            Debug.Log($"[OverlayChanger] OnOverlayChanged {controllerType}");
            switch (controllerType)
            {
                case ControllerType.MouseKeys:
                    _mouseKeyboardOverlay.Show();
                    _xBoxOverlay.Hide();
                    _xBoxOverlay.Hide();
                    break;
                case ControllerType.XBox:
                    _mouseKeyboardOverlay.Hide();
                    _xBoxOverlay.Show();
                    _psOverlay.Hide();
                    break;
                case ControllerType.PS:
                    _mouseKeyboardOverlay.Hide();
                    _xBoxOverlay.Hide();
                    _psOverlay.Show();
                    break;
                default:
                    _mouseKeyboardOverlay.Show();
                    _xBoxOverlay.Hide();
                    _xBoxOverlay.Hide();
                    break;
            }
        }

         
    }
}