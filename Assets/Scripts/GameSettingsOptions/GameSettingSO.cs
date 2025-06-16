using UnityEngine;

namespace GameSettingsOptions
{
    public abstract class GameSettingSO : ScriptableObject
    {
        [SerializeField] private string _optionLabel;
        
        public string OptionLabel => _optionLabel;
    }
}