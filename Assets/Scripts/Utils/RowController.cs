using System;
using CustomUi;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  
using TMPro;
using UnityEngine.InputSystem;


namespace Utils
{   
    [RequireComponent(typeof(Image))] 
    public class RowController : MonoBehaviour , ISelectHandler, IDeselectHandler, IMoveHandler
    {
    [SerializeField] private RowOptionsHandler _rowOptionsHandler;
        // --- Inspector Assigned References ---
    [Header("Row Elements")]
    [SerializeField] private Image _targetGraphic; // The Image component on this GameObject for visual feedback
    [SerializeField] private TMP_Text _settingNameText; // The TextMeshProUGUI component for the setting name
    [SerializeField] private Button _actionButton;     // The actual Button component inside this row
    [SerializeField] private Slider _actionSlider; // <--- This MUST be assigned in the Inspector
    
    [Header("Transition Settings")]
    [SerializeField] private float _fadeDuration = 0.1f; // How long color transition takes

    [Header("Pulsing border effect")]
    public Image _glowImage; // Assign via Inspector
    [SerializeField] private float minAlpha = 0f;
    [SerializeField] private float maxAlpha = 1f;
    [SerializeField] private float pulseDuration = .4f;
    
    private Tween _pulseTween;
  //  private bool _isHovered = false;
    private bool _isSelected = false;

    [SerializeField] private InputActionAsset _inputActions;
    private InputAction _navigateAction;
    private InputAction _clickAction;
    private InputAction _scrollWheelAction;
    
    private void Awake()
    {
        _navigateAction = _inputActions.FindAction("UI/Navigate");  
        _clickAction = _inputActions.FindAction("UI/Click", true);
        _scrollWheelAction = _inputActions.FindAction("UI/ScrollWheel", true);
        _rowOptionsHandler.Initialize();
     //   _navigateAction.performed += OnNavigate;
        _clickAction.performed += OnMouseClick;
        _scrollWheelAction.performed += OnScrollWheel;
        _navigateAction.Enable();
    }

    private void OnScrollWheel(InputAction.CallbackContext context)
    {
        if (!_isSelected) return;
        
        float scrollY = context.ReadValue<Vector2>().y;

        if (scrollY > 0)
        {
            _rowOptionsHandler.OnScrollWheelUp();
        }
        else if (scrollY < 0)
        {
            _rowOptionsHandler.OnScrollWheelDown();
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (_isSelected) return;
        
        Debug.Log($"[RowController] OnSelect");
        HighlightRow(true);
        _settingNameText.color = Color.yellow;
        _isSelected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (!_isSelected) return;
        Debug.Log($"[RowController] OnDeselect");
        HighlightRow(false);
        _settingNameText.color = Color.white;
        _isSelected = false;
    }

    public void OnSubmit(BaseEventData eventData)
    {
        Debug.Log($"[RowController] OnSubmit");
        _rowOptionsHandler.OnSubmit();
    }
 
    private void HighlightRow(bool on)
    {
        if (on)
        {
            if (_pulseTween == null)
            {
                _pulseTween = _glowImage.DOFade(maxAlpha, pulseDuration)
                    .SetLoops(-1, LoopType.Yoyo)
                    .From(minAlpha)
                    .SetEase(Ease.InOutSine)
                    .SetAutoKill(false)
                    .Pause();  
            }

            _pulseTween.Restart();
        }
        else
        {
            _pulseTween?.Pause();
            var color = _glowImage.color;
            color.a = 0f;
            _glowImage.color = color;
        }
    }

    public void OnMove(AxisEventData eventData)
    {
        if (eventData.moveDir == MoveDirection.Left)
        {
            _rowOptionsHandler.OnLeftArrow();
        }
        else if (eventData.moveDir == MoveDirection.Right)
        {
            _rowOptionsHandler.OnRightArrow();
        }
    }

   
    private void OnMouseClick(InputAction.CallbackContext obj)
    {
        
        
    }

    }
}