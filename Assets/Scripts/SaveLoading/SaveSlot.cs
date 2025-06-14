using System;
using UnityEngine;
using UnityEngine.UI;

namespace SaveLoading
{
    public class SaveSlot: MonoBehaviour
    {
        public event Action<SaveSlot> OnSlotPicked = default;
        
        [SerializeField] private Image _image;
        public Image Image => _image;

        public void PickSaveSlot()
        {
            OnSlotPicked?.Invoke(this);
        }
    }
}