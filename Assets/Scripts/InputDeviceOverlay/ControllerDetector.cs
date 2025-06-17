using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

     private void Start()
     {
      OnGamepadStatusChanged();
     }

     private void OnEnable()
     {
      _clickAction = _inputActions.FindAction("UI/Click", true);
      _clickAction.performed += OnMouseClick;
      _clickAction.Enable();  
      
      _anyKeyAction = new InputAction(type: InputActionType.PassThrough, binding: "<Keyboard>/anyKey");
      _anyKeyAction.performed += OnAnyKeyPressed;
      _anyKeyAction.Enable();
      
      _anyGamepadButtonAction = new InputAction( type: InputActionType.PassThrough, binding: "<Gamepad>/*");
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
       Debug.Log("[ControllerDetector] It's a PlayStation controller.");
       OnControlsChanged(ControllerType.PS);
      }
      else if (layout.Contains("XInput") || displayName.ToLower().Contains("xbox"))
      {
       Debug.Log("[ControllerDetector] It's an Xbox controller.");
       OnControlsChanged(ControllerType.XBox);
      }
     }

     private void OnControlsChanged(ControllerType active)
     {
      _currentControlsType = active;
      OnControllerChanged?.Invoke(_currentControlsType);
     }

     private void OnMouseClick(InputAction.CallbackContext obj)
     {
      OnControlsChanged(ControllerType.MouseKeys);
     }
     
     
     private void OnAnyKeyPressed(InputAction.CallbackContext obj)
     {
      Debug.Log("[ControllerDetector] OnAnyKeyPressed");
      OnControlsChanged(ControllerType.MouseKeys);
     }
     
     
     private void OnAnyGamepadButtonPressed(InputAction.CallbackContext obj)
     {
      Debug.Log("[ControllerDetector] OnAnyGamepadButtonPressed");
      OnGamepadStatusChanged();
     }

     public void ForceUpdateStatus()
     {
      OnGamepadStatusChanged();
      ;
     }
 }

}