using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class HighlightAnimator: MonoBehaviour
    {
        [SerializeField] private Image _target; // Assign via Inspector
        [SerializeField] private PulseSettingsSO _pulseSettings;
      
        private Tween _pulseTween;
        
        public void HighlightRow(bool on)
        {
            if (on)
            {
                if (_pulseTween == null)
                {
                    _pulseTween = _target.DOFade(_pulseSettings.MaxAlpha, _pulseSettings.PulseDuration)
                        .SetLoops(-1, LoopType.Yoyo)
                        .From(_pulseSettings.MinAlpha)
                        .SetEase(Ease.InOutSine)
                        .SetAutoKill(false)
                        .Pause();  
                }

                _pulseTween.Restart();
            }
            else
            {
                _pulseTween?.Pause();
                var color = _target.color;
                color.a = 0f;
                _target.color = color;
            }
        }
    }
}