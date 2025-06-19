using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace InputDeviceOverlay
{
    public class OverlayChanger: MonoBehaviour
    { 
        [SerializeField] private float _fadeDuration = 0.2f;
        [SerializeField] private List<OverlayUnit> _overlayUnitsList = new ();
        
         private Dictionary<ControllerType, OverlayUnit> _overlayUnits = new ();
         private ControllerType _controllerTypeCurrent;
         private Tween _currentTween;
         
         
         private void OnValidate()
         {
             _overlayUnitsList = new List<OverlayUnit>(GetComponentsInChildren<OverlayUnit>());
         }

         private void Awake()
         {
             foreach (var unit in _overlayUnitsList)
             {
                 _overlayUnits.Add(unit.ControllerType, unit);
             }
         }
 
        private void Start()
        {
            ControllerDetector.Instance.OnControllerChanged += OnOverlayChanged;
            _controllerTypeCurrent = ControllerDetector.Instance.CurrentControlsType;
            ControllerDetector.Instance.ForceUpdateStatus();
            _overlayUnits[_controllerTypeCurrent].CanvasGroup.alpha = 1f;
            _overlayUnits[_controllerTypeCurrent].Show();
        }

        private void OnOverlayChanged(ControllerType controllerType)
        {
            
           _overlayUnits[_controllerTypeCurrent].Hide(); 
           
            SwitchOverlay(_overlayUnits[controllerType].CanvasGroup);
           _controllerTypeCurrent = controllerType;
           _overlayUnits[_controllerTypeCurrent].Show();
        }
      
        private void SwitchOverlay(CanvasGroup target)
        {
            _currentTween?.Kill();
            _currentTween = target.DOFade(1f, _fadeDuration);
        }
    }
}