using UnityEngine;
using UnityEngine.EventSystems;

namespace Screens
{
    public class MainMenuScreen: MenuScreen
    {
        //[SerializeField] private GameObject _firstSelected;
        
        void OnEnable() {
       //     Debug.Log($"[MainMenuScreen] OnEnable");
          //  EventSystem.current.SetSelectedGameObject(null); // reset first
         //   EventSystem.current.SetSelectedGameObject(_firstSelected);
        }
    }
}