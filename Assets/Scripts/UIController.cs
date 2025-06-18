using System.Collections.Generic;
using Screens;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ScreenType { Title, MainMenu, SaveLoad, Settings, Credits }
public enum TabType { None, LoadsScroll, LoadSave, NotImplemented, GameSettings, GameControls, VideoSettings, SoundSettings, mainMenuIntro, QuitGame, Intro, CreditsMain }

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    
    [SerializeField] private MenuScreen titleScreen;
    [SerializeField] private MenuScreen mainScreen;
    [SerializeField] private MenuScreen saveLoadScreen;
    [SerializeField] private MenuScreen settingsScreen;
    [SerializeField] private MenuScreen CreditsScreen;
    
    
    [SerializeField] protected InputActionAsset _inputActions;
    protected InputAction _goBackAction;
    
    private Dictionary<ScreenType, MenuScreen> screens = default;

    private MenuScreen _activeScreen;
    void Awake() {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }
        
        _goBackAction = _inputActions.FindAction("UI/Cancel");
        _goBackAction.performed += OnCancelClick;
        _goBackAction.Enable();
        
        screens = new Dictionary<ScreenType, MenuScreen> {
            {ScreenType.Title, titleScreen},
            {ScreenType.MainMenu, mainScreen},
            {ScreenType.SaveLoad, saveLoadScreen},
            {ScreenType.Settings, settingsScreen},
            {ScreenType.Credits, CreditsScreen}
        };
        
        ShowScreen(ScreenType.Title);
    }

    private void OnCancelClick(InputAction.CallbackContext obj)
    {
        _activeScreen.OnCancelClick();
    }

    public void ShowScreen(ScreenType type) {
        foreach (var kv in screens) {
            bool isActive = (kv.Key == type);
            if (isActive)
            {
                kv.Value.Show();
                kv.Value.IsActive = true;
                _activeScreen = kv.Value;
            }
            else
            {
                kv.Value.Hide();
                kv.Value.IsActive = false;
            }
        }
    }
    
    public void ShowScreen(int enumNum)
    {
        if (System.Enum.IsDefined(typeof(ScreenType), enumNum))
        {
            ScreenType type = (ScreenType)enumNum;
            ShowScreen(type);
        }
        else
        {
            Debug.LogError($"[UIManager]: Invalid ScreenType enum number provided: {enumNum}");
        }
    }
    
    public void QuitGame() { Application.Quit();}
}
