using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Utils
{
    //hovers over active ui elements. enables correct hoverings with keys or with mouse
    public class SelectionController: MonoBehaviour
    {
        public event Action<bool,GameObject> OnSelectionChanged;
        
        private PointerEventData _pointerEventData;
        private List<RaycastResult> _raycastResults = new();
        private Vector3 _lastMousePosition;
        [SerializeField] private InputActionAsset _inputActions;
        private InputAction _navigateAction;
        private InputAction _clickAction;
        private bool _inputWasFromController = false;
        private GameObject _selectedObject;
        
        void Awake()
        {
            _navigateAction = _inputActions.FindAction("UI/Navigate");  
            _clickAction = _inputActions.FindAction("UI/Click", true);
            _navigateAction.performed += OnNavigate;
            _clickAction.performed += OnMouseClick;
            _navigateAction.Enable();
        }
        
        private void OnNavigate(InputAction.CallbackContext ctx)
        {
            _inputWasFromController = true;
        }
        
        private void OnMouseClick(InputAction.CallbackContext context)
        {
           _inputWasFromController = false;
        }

        
        void Update()
        {
            ActualizeCurrentSelection();
            if (!Input.mousePresent) return;
        
            PrioritizeCurrentSelection();
        }

        private void PrioritizeCurrentSelection()
        {
            if (_inputWasFromController)
            {
                if (IsMouseOverSelectableUI(out GameObject hovered))
                {
                    if (EventSystem.current.currentSelectedGameObject != hovered)
                    {
                        ClearMouseHover(hovered);
                        EventSystem.current.SetSelectedGameObject(EventSystem.current.currentSelectedGameObject);
                    }
                }
                return;
            } 

            if (_lastMousePosition != Input.mousePosition)
            {
                _lastMousePosition = Input.mousePosition;
                _inputWasFromController = false;

                if (IsMouseOverSelectableUI(out GameObject hovered))
                {
                    if (EventSystem.current.currentSelectedGameObject != hovered)
                    {
                        if (_inputWasFromController)
                        {
                            EventSystem.current.SetSelectedGameObject(EventSystem.current.currentSelectedGameObject);
                        }
                        else
                        {
                            EventSystem.current.SetSelectedGameObject(hovered);
                        }
                    }
                }
            }
        }

        private bool IsMouseOverSelectableUI(out GameObject hoveredObject)
        {
            hoveredObject = null;
            _pointerEventData ??= new PointerEventData(EventSystem.current);
            _pointerEventData.position = Input.mousePosition;

            _raycastResults.Clear();
            EventSystem.current.RaycastAll(_pointerEventData, _raycastResults);

            foreach (var result in _raycastResults)
            {
                var selectable = result.gameObject.GetComponent<Selectable>();
                if (selectable != null && selectable.interactable && selectable.enabled)
                {
                    hoveredObject = result.gameObject;
                    return true;
                }
            }

            return false;
        }
        
        private void ClearMouseHover(GameObject hoveredObject)
        {
            var pointerExitHandlers = hoveredObject.GetComponents<IPointerExitHandler>();
            var pointerEventData = new PointerEventData(EventSystem.current);

            foreach (var handler in pointerExitHandlers)
            {
                handler.OnPointerExit(pointerEventData);
            }
        }

        private void ActualizeCurrentSelection()
        {
            bool isSelected = false;
            if (EventSystem.current == null)
            {
                Debug.LogWarning("[SelectionController ActualizeCurrentSelection]  No EventSystem found in the scene.");
                OnSelectionChanged?.Invoke(false, null);
                return;
            }

            var selectedTemp = EventSystem.current.currentSelectedGameObject;
            if (selectedTemp == _selectedObject) return;
            if (selectedTemp == null && _selectedObject == null) return;
            _selectedObject = selectedTemp;

            OnSelectionChanged?.Invoke(_selectedObject != null, _selectedObject);
        }
    }
}
