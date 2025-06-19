using UnityEngine;


[CreateAssetMenu(fileName = "UiVfxSettingsSO", menuName = "Scriptable Objects/UiVfxSettingsSO SO")]
public class UiVfxSettingsSO : ScriptableObject
{
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _transitionDuration;
    [SerializeField] private float _fadeDuration = .2f;
    
    public float RotationSpeed => _rotationSpeed;
    public float TransitionDuration => _transitionDuration;
    public float FadeDuration => _fadeDuration;
}
