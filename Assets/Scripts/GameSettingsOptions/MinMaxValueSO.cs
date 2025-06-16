using UnityEngine;

namespace GameSettingsOptions
{
    [CreateAssetMenu(fileName = "MinMaxValueSO", menuName = "Scriptable Objects/Min Max Value SO")]
    public class MinMaxValueSO: GameSettingSO
    {
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;
        [SerializeField] private float _defaultValue;
        [SerializeField] private float _scrollStep;
        
        public float MinValue => _minValue;
        public float MaxValue => _maxValue;
        public float DefaultValue => _defaultValue;
        public float ScrollStep => _scrollStep;
    }
}