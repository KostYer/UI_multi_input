using UnityEngine;

namespace Utils
{
    public class TransformRotator: MonoBehaviour
    {
     [SerializeField] private UiVfxSettingsSO _settings;

     private float _timer = 0f;
     private int _currentAxis = 0; // 0 = X, 1 = Y, 2 = Z

     private int _signCurrent = 1;
     private int _signNext = 1;

     void Start()
     {
         _signCurrent = RandomSign();
         _signNext = RandomSign();
     }

     void Update()
     {
         _timer += Time.deltaTime;
         if (_timer > _settings.TransitionDuration)
         {
             _timer -=  _settings.TransitionDuration;
             _currentAxis = (_currentAxis + 1) % 3;

             // Flip sign randomly on each axis switch
             _signCurrent = _signNext;
             _signNext = RandomSign();
         }

         float t = _timer /  _settings.TransitionDuration; 

         float ease = Mathf.Sin(t * Mathf.PI);

         Vector3 rotation = Vector3.zero;

         // Current axis fades out with its sign
         float currentSpeed = _settings.RotationSpeed * Mathf.Sin((1 - t) * Mathf.PI) * _signCurrent;

         // Next axis fades in with its sign
         int nextAxis = (_currentAxis + 1) % 3;
         float nextSpeed = _settings.RotationSpeed * ease * _signNext;

         rotation[_currentAxis] = currentSpeed;
         rotation[nextAxis] = nextSpeed;

         transform.Rotate(rotation * Time.deltaTime, Space.Self);
     }

     int RandomSign()
     {
         return (Random.value > 0.5f) ? 1 : -1;
     }
    }
}