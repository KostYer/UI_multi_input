using System;
using UnityEngine;

namespace Utils
{

    [RequireComponent(typeof(CanvasGroup))]
    public class CanvasGroupWrapper : MonoBehaviour
    {
        
        [SerializeField] private CanvasGroup _canvasGroup;
        public CanvasGroup CanvasGroup => _canvasGroup;
        
        private void OnValidate()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Show()
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}
