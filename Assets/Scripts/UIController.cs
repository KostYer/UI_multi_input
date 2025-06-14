using System.Collections;
using System.Collections.Generic;
using Screens;
using UnityEngine;


public enum ScreenType { Title, MainMenu, SaveLoad, Settings, Credits }

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    
    [SerializeField] private MenuScreen titleScreen;
    [SerializeField] private MenuScreen mainScreen;
    [SerializeField] private MenuScreen saveLoadScreen;
    [SerializeField] private MenuScreen settingsScreen;
    [SerializeField] private MenuScreen CreditsScreen;
    
    private Dictionary<ScreenType, MenuScreen> screens = default;

    void Awake() {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); return; }
        
        screens = new Dictionary<ScreenType, MenuScreen> {
            {ScreenType.Title, titleScreen},
            {ScreenType.MainMenu, mainScreen},
            {ScreenType.SaveLoad, saveLoadScreen},
            {ScreenType.Settings, settingsScreen},
            {ScreenType.Credits, CreditsScreen}
        };
        ShowScreen(ScreenType.MainMenu);
    }

    public void ShowScreen(ScreenType type) {
        foreach (var kv in screens) {
            bool isActive = (kv.Key == type);
            if (isActive)
            {
                kv.Value.Show();
            }
            else
            {
                kv.Value.Hide();
            }


        }
    }
}
