using UnityEngine;
using UnityEngine.EventSystems;
using Minesweeper.Event;

namespace Minesweeper.UI
{
    public class UIElementEventController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        [SerializeField] private VoidEvent PointerEnter;
        [SerializeField] private VoidEvent PointerClick;

        public void OnPointerEnter(PointerEventData eventData) => PointerEnter.Raise();
        
        public void OnPointerClick(PointerEventData eventData) => PointerClick.Raise();
    }
}