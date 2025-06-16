using Screens;
using UnityEngine;
using UnityEngine.UI;

namespace SaveLoading
{
    public class LoadTab: ScreenTab
    {
        [SerializeField] private Image _saveImage;
        
        public void Init(SaveSlot saveSlot)
        {
            _saveImage.sprite = saveSlot.Image.sprite;
        }
    }
}