using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Minesweeper.Event;
using Minesweeper.Animation;

namespace Minesweeper.UI
{
    public class ButtonController : MonoBehaviour, 
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [Header("Event")]
        [Tooltip("Click is triggered if the pointer has been inside the button when it was both down and up.")]
        [SerializeField] private List<VoidEvent> _clickEvents;

        [Tooltip("Down is triggered when the pointer is down inside the button.")]
        [SerializeField] private List<VoidEvent> _downEvents;

        [Tooltip("Enter is triggered when the pointer enters the bounds of the button.")]
        [SerializeField] private List<VoidEvent> _enterEvents;

        [Tooltip("Exit is triggered when the pointer leaves the bounds of the button.")]
        [SerializeField] private List<VoidEvent> _exitEvents;

        // [Header("Animtation")]

        public void OnPointerClick(PointerEventData eventData)
        {
            foreach (var clickEvent in _clickEvents)
            {
                clickEvent?.Raise();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            foreach (var downEvent in _downEvents)
            {
                downEvent?.Raise();
            }
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            foreach (var enterEvent in _enterEvents)
            {
                enterEvent?.Raise();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            foreach (var exitEvent in _exitEvents)
            {
                exitEvent?.Raise();
            }
        }
    }
}
