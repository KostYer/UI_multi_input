using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSettingsOptions
{

    [CreateAssetMenu(fileName = "MultipleChoiceSO", menuName = "Scriptable Objects/Multiple Choice Scriptable object")]
    public class MultipleChoiceSO : GameSettingSO
    {
        [SerializeField] private string[] _options;
        [SerializeField] private int _defaultIndex;
      
        public virtual string[] Options => _options;
        public int DefaultIndex => _defaultIndex;
    }
}