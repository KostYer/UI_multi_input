using UnityEngine;


[CreateAssetMenu(fileName = "UiVfxSettingsSO", menuName = "Scriptable Objects/UiVfxSettingsSO SO")]
public class UiVfxSettingsSO : ScriptableObject
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _transitionDuration;
    
    public float RotationSpeed => _rotationSpeed;
    public float TransitionDuration => _transitionDuration;
}
