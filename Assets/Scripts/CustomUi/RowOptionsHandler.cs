using GameSettingsOptions;
using UnityEngine;

namespace CustomUi
{
    public abstract class RowOptionsHandler: MonoBehaviour
    {
        public abstract void Initialize();
        public abstract void OnSubmit();
        public abstract void OnRightArrow();
        public abstract void OnLeftArrow();
        public abstract void OnScrollWheelUp();
        public abstract void OnScrollWheelDown();

    }
}