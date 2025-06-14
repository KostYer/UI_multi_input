using System;
using UnityEngine;
using Utils;

namespace Screens
{
    public abstract class MenuScreen: MonoBehaviour
    {
        [SerializeField] private CanvasGroupWrapper _canvasGroupWrapper;
        
        private void OnValidate()
        {
            _canvasGroupWrapper = GetComponent<CanvasGroupWrapper>();
        }
        
        public virtual void Show()
        {
            _canvasGroupWrapper.Show();
        }

        public virtual void Hide()
        {
            _canvasGroupWrapper.Hide();
        }
    }
}