using CustomUi;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.InputSystem;

namespace Utils
{
    [RequireComponent(typeof(Image))]
    public class RowController : MonoBehaviour, ISelectHandler, IDeselectHandler, IMoveHandler
    {
        [SerializeField] private RowOptionsHandler _rowOptionsHandler;
        [SerializeField] private HighlightAnimator _highlightAnimator;
        [SerializeField] private InputActionAsset _inputActions;

        [Header("Row Elements")] [SerializeField]
        private Image _targetGraphic;

        [SerializeField] private TMP_Text _settingNameText;

        private Tween _pulseTween;
        private bool _isSelected = false;
        private InputAction _navigateAction;
        private InputAction _scrollWheelAction;

        private void Awake()
        {
            _navigateAction = _inputActions.FindAction("UI/Navigate");
            _scrollWheelAction = _inputActions.FindAction("UI/ScrollWheel", true);
            _rowOptionsHandler.Initialize();
            _scrollWheelAction.performed += OnScrollWheel;
            _navigateAction.Enable();
        }

        private void OnValidate()
        {
            _highlightAnimator = GetComponent<HighlightAnimator>();
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

            _highlightAnimator.HighlightRow(true);
            _settingNameText.color = new Color(1f, 1f, .75f);
            _isSelected = true;
        }

        public void OnDeselect(BaseEventData eventData)
        {
            if (!_isSelected) return;
            _highlightAnimator.HighlightRow(false);
            _settingNameText.color = Color.white;
            _isSelected = false;
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
    }
}