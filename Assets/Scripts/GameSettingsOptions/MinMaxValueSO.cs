using UnityEngine;

namespace GameSettingsOptions
{
    [CreateAssetMenu(fileName = "MinMaxValueSO", menuName = "Scriptable Objects/Min Max Value SO")]
    public class MinMaxValueSO: GameSettingSO
    {
        private float _minValue;
        private float _maxValue;
        
        public float MinValue => _minValue;
        public float MaxValue => _maxValue;
    }
}