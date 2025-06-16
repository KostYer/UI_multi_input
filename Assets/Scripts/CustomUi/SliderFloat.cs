using CustomUi;
using GameSettingsOptions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderFloat : RowOptionsHandler
{
    [SerializeField] private MinMaxValueSO _settingsSO;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _optionLabel;
    [SerializeField] private TMP_Text _currentValue;

    private void OnValidate()
    {
        Initialize();
    }

    public override void Initialize()
    {
        _optionLabel.text = " " + _settingsSO.OptionLabel;

        _slider.wholeNumbers = false;
        _slider.minValue = _settingsSO.MinValue;
        _slider.maxValue = _settingsSO.MaxValue;
        _slider.value = _settingsSO.DefaultValue;
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
    
    public override void OnScrollWheelUp()
    {
        if (_slider.value >= _slider.maxValue) return;
        _slider.value += _settingsSO.ScrollStep;
        UpdateOptionText();
    }

    public override void OnScrollWheelDown()
    {
        if (_slider.value <= _slider.minValue) return;
        _slider.value -= _settingsSO.ScrollStep;
        UpdateOptionText();
    }

    private void IncreaseValue()
    {
        if (_slider.value >= _slider.maxValue) return;
        _slider.value++;
        UpdateOptionText();
    }

    private void DecreaseValue()
    {
        if (_slider.value <= _slider.minValue) return;
        _slider.value--;
        UpdateOptionText();
    }

    private void UpdateOptionText()
    {
        _currentValue.text = _slider.value.ToString();
    }
}