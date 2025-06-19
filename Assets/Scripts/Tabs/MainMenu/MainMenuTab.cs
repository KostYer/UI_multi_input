using System.Collections;
using DG.Tweening;
using Screens;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuTab : ScreenTab
{
  
   [SerializeField] private CanvasGroup _rawImage;
   [SerializeField] private UiVfxSettingsSO _settingsSO;

   private Tween _fadeTween;

   public void FadeTo(float targetAlpha)
   {
      if (_rawImage == null) return;

      _fadeTween?.Kill();

      _fadeTween = _rawImage.DOFade(targetAlpha, _settingsSO.FadeDuration)
         .SetEase(Ease.InOutSine)
         .SetUpdate(true);  
   }

   
 
   public override void Show()
   {
      _rawImage.alpha = 0;
      base.Show();
      FadeTo(1);
   }

   public override void Hide()
   {
      base.Hide();
      _rawImage.alpha = 0;
   }
}
