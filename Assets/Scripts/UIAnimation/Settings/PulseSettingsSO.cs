using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PulseSettingsSO", menuName = "Scriptable Objects/PulseSettings SO")]
public class PulseSettingsSO : ScriptableObject
{
    [SerializeField] private float _minAlpha = 0f;
    [SerializeField] private float _maxAlpha = 1f;
    [SerializeField] private float _pulseDuration = .8f;
    
    public float MinAlpha => _minAlpha;
    public float MaxAlpha => _maxAlpha;
    public float PulseDuration => _pulseDuration;
}
