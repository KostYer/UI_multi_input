using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Screens
{
    public class MainMenuScreen : MenuScreen
    {
        [SerializeField] private GameObject _firstSelected;

        void OnEnable()
        {
            Debug.Log("[MainMenuScreen] OnEnable");
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(_firstSelected);
        }

      
    }
}