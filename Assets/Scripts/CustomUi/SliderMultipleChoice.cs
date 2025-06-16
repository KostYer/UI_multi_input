using System;
using GameSettingsOptions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CustomUi
{
    public class SliderMultipleChoice: RowOptionsHandler
    {
        [SerializeField] private MultipleChoiceSO _multipleChoiceSO;
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _optionLabel;
        [SerializeField] private TMP_Text _currentValue;
        
        private void OnValidate()
        {
            Initialize();
        }
        public override void Initialize()
        {
            _optionLabel.text = " " +_multipleChoiceSO.OptionLabel;
            
            _slider.wholeNumbers = true;
            _slider.minValue = 0;
            _slider.maxValue = _multipleChoiceSO.Options.Length -1;
            _slider.value = _multipleChoiceSO.DefaultIndex;
            UpdateOptionText();
        }

        public override void OnSubmit()
        {
            IncreaseValue();
        }

        public override void OnRightArrow()
        {
            IncreaseValue();
        }

        public override void OnLeftArrow()
        {
            DecreaseValue();
        }

        private void IncreaseValue()
        {
            if( _slider.value >= _slider.maxValue) return;
            _slider.value++;
            UpdateOptionText();
        }
        
        private void DecreaseValue()
        {
            if( _slider.value <= _slider.minValue) return;
            _slider.value--;
            UpdateOptionText();
        }

        private void UpdateOptionText()
        {
            _currentValue.text = _multipleChoiceSO.Options[(int)_slider.value];
        }
    }
}