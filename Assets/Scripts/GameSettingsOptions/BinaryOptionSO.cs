using UnityEngine;

namespace GameSettingsOptions
{
    [CreateAssetMenu(fileName = "BinaryOptionSO", menuName = "Scriptable Objects/Binary SO")]
    public class BinaryOptionSO: GameSettingSO
    {
        [SerializeField] private string[] _options = new string[2];
        [Range(0,1)]
        [SerializeField] private int _defaultIndex;
      
        public virtual string[] Options => _options;
        public int DefaultIndex => _defaultIndex;
    }
}