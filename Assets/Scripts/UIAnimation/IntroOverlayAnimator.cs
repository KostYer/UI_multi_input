using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace
{
    public class IntroOverlayAnimator: MonoBehaviour
    {
        [SerializeField] private PulseSettingsSO _pulseSettings;
        [SerializeField] private CanvasGroup _canvasGroup;
      
        private Tween _pulseTween;

        public void Animate(bool on)
        {
            if (on)
            {
                if (_pulseTween == null)
                {
                    _pulseTween = _canvasGroup.DOFade(_pulseSettings.MaxAlpha, _pulseSettings.PulseDuration)
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
                _canvasGroup.alpha = 0;
            }
        }
    }
}