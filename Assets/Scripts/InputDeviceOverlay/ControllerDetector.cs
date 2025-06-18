using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace InputDeviceOverlay
{
 public enum ControllerType {MouseKeys, XBox, PS}

 public class ControllerDetector : MonoBehaviour
 {
  public static ControllerDetector Instance;
  
  public event Action<ControllerType> OnControllerChanged;
  [SerializeField] private InputActionAsset _inputActions;
   private InputAction _clickAction;
   private InputAction _anyKeyAction;
   private InputAction _anyGamepadButtonAction;
   private ControllerType _currentControlsType;
   
   public ControllerType CurrentControlsType => _currentControlsType;
   public bool IsGamepadActive => _currentControlsType != ControllerType.MouseKeys;
    
     private void Awake()
     {
      if (Instance == null)
      { Instance = this; DontDestroyOnLoad(gameObject); }
      else { Destroy(gameObject); return; }
     }
 
     private void OnEnable()
     {
      _clickAction = _inputActions.FindAction("UI/Click", true);
      _clickAction.performed += OnMouseClick;
      _clickAction.Enable();  
      
      _anyKeyAction = new InputAction(type: InputActionType.PassThrough, binding: "<Keyboard>/anyKey");
      _anyKeyAction.performed += OnAnyKeyPressed;
      _anyKeyAction.Enable();
      
      _anyGamepadButtonAction = new InputAction(
       type: InputActionType.PassThrough
      );

      _anyGamepadButtonAction.AddBinding("<Gamepad>/buttonSouth");
      _anyGamepadButtonAction.AddBinding("<Gamepad>/buttonNorth");
      _anyGamepadButtonAction.AddBinding("<Gamepad>/buttonEast");
      _anyGamepadButtonAction.AddBinding("<Gamepad>/buttonWest");
      _anyGamepadButtonAction.AddBinding("<Gamepad>/dpad/up");
      _anyGamepadButtonAction.AddBinding("<Gamepad>/dpad/down");
      _anyGamepadButtonAction.AddBinding("<Gamepad>/dpad/left");
      _anyGamepadButtonAction.AddBinding("<Gamepad>/dpad/right");
      _anyGamepadButtonAction.AddBinding("<Gamepad>/leftShoulder");
      _anyGamepadButtonAction.AddBinding("<Gamepad>/rightShoulder");
      
      _anyGamepadButtonAction.performed += OnAnyGamepadButtonPressed;
      _anyGamepadButtonAction.Enable();
     }

     private void OnDisable()
     {
      _clickAction.performed -= OnMouseClick;
      _anyKeyAction.performed -= OnAnyKeyPressed;
      _anyGamepadButtonAction.performed -= OnAnyGamepadButtonPressed;
     }

      private void OnGamepadStatusChanged()
     {
      var gamepad = Gamepad.current;
      if (gamepad == null)
      {
       OnControlsChanged(ControllerType.MouseKeys);
       return;
      }

      string layout = gamepad.layout;
      string displayName = gamepad.displayName;

      if (layout.Contains("DualShock") || displayName.ToLower().Contains("playstation") || displayName.ToLower().Contains("dual"))
      {
       OnControlsChanged(ControllerType.PS);
      }
      else if (layout.Contains("XInput") || displayName.ToLower().Contains("xbox"))
      {
       OnControlsChanged(ControllerType.XBox);
      }
     } 

     private void OnControlsChanged(ControllerType active)
     {
      if(_currentControlsType == active) return;
      _currentControlsType = active;
      OnControllerChanged?.Invoke(_currentControlsType);
     }

     private void OnMouseClick(InputAction.CallbackContext obj)
     {
      OnControlsChanged(ControllerType.MouseKeys);
     }
     
     private void OnAnyKeyPressed(InputAction.CallbackContext obj)
     {
      OnControlsChanged(ControllerType.MouseKeys);
     }
     
     private void OnAnyGamepadButtonPressed(InputAction.CallbackContext ctx)
     {
      var control = ctx.control;

      if (control is ButtonControl button && button.wasPressedThisFrame)
      {
       OnGamepadStatusChanged();
      }
     
     }

     public void ForceUpdateStatus()
     {
      OnGamepadStatusChanged();
     }
 }

}