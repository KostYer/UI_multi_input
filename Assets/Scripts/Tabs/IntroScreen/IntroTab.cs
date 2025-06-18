using System;
using System.Collections;
using DefaultNamespace;
using Screens;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Tabs.IntroScreen
{
    public class IntroTab: ScreenTab
    {
       [SerializeField] private IntroOverlayAnimator _overlayAnimator;
       [SerializeField] private float _fadeDuration = 0.5f;
       [SerializeField] private CanvasGroup _mainCanvasGroup;
       private InputAction _submitAction;
       
       protected override void Awake()
       {
           base.Awake();

           TryInitAction();
       }

       private void OnSubmit(InputAction.CallbackContext obj)
       {
           StartCoroutine(FadeCoroutine(() => UIController.Instance.ShowScreen(ScreenType.MainMenu)));
       }

       public override void Show()
       {
           base.Show();
           TryInitAction();
           _overlayAnimator.Animate(true);
       }

       public override void Hide()
       {
           base.Hide();
           _overlayAnimator.Animate(false);
           _submitAction.performed -= OnSubmit;
       }

       private void TryInitAction()
       {
           if (_submitAction != null) return;
           
           _submitAction = _inputActions.FindAction("UI/Submit");
           _submitAction.Enable();
           _submitAction.performed += OnSubmit;
       }

       private IEnumerator FadeCoroutine(Action onFinished)
       {
           float elapsed = 0f;

           while (elapsed < _fadeDuration)
           {
               elapsed += Time.unscaledDeltaTime;
               _mainCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / _fadeDuration);
               yield return null;
           }

           _mainCanvasGroup.alpha = 0f;
           onFinished.Invoke();
       }
    }
}