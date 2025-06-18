using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CustomUi
{
    public class OnSelectAction : MonoBehaviour, ISelectHandler
    {
        public UnityEvent onSelect;

        public void OnSelect(BaseEventData eventData)
        {
            onSelect?.Invoke();
        }
    }
}