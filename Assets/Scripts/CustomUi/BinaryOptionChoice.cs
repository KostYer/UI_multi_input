using GameSettingsOptions;
using TMPro;
using UnityEngine;

namespace CustomUi
{
    public class BinaryOptionChoice: RowOptionsHandler
    {
        [SerializeField] private BinaryOptionSO _options;
        [SerializeField] private TMP_Text _optionLabel;
        [SerializeField] private TMP_Text _currentValue;

        private bool _isOn;
        
        private void OnValidate()
        {
            Initialize();
        }
        public override void Initialize()
        {
            _optionLabel.text = " " +_options.OptionLabel;
            _isOn = _options.DefaultIndex == 0;
            UpdateOptionText();
        }

        public override void OnSubmit()
        {
            ChangeValue();
        }

        public override void OnRightArrow()
        {
            ChangeValue();
        }

        public override void OnLeftArrow()
        {
            ChangeValue();
        }

        public override void OnScrollWheelUp()
        {
            ChangeValue();
        }

        public override void OnScrollWheelDown()
        {
            ChangeValue();
        }
 
        private void ChangeValue()
        {
            _isOn = !_isOn;
        }

        private void UpdateOptionText()
        {
            _currentValue.text = _isOn ? _options.Options[0] : _options.Options[1];
        }
    }
}