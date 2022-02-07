using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Minesweeper.Event;
using Minesweeper.Animation;

namespace Minesweeper.UI
{
    public class ButtonController : MonoBehaviour, 
        IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [Header("Component")]
        [SerializeField] private Image _BGImage;
        [SerializeField] private RectTransform _BGRectTransform;
        [SerializeField] private TextMeshProUGUI _text;

        [Header("Event")]
        [Tooltip("Click is triggered if the pointer has been inside the button when it was both down and up.")]
        [SerializeField] private List<VoidEvent> _clickEvents;

        [Tooltip("Down is triggered when the pointer is down inside the button.")]
        [SerializeField] private List<VoidEvent> _downEvents;

        [Tooltip("Enter is triggered when the pointer enters the bounds of the button.")]
        [SerializeField] private List<VoidEvent> _enterEvents;

        [Tooltip("Exit is triggered when the pointer leaves the bounds of the button.")]
        [SerializeField] private List<VoidEvent> _exitEvents;

        [Header("Animtation")]
        [SerializeField] private ImageColorTo _BGColorOnDown;
        [SerializeField] private ImageColorTo _BGColorOnEnter;
        [SerializeField] private ImageColorTo _BGColorOnExit;
        [SerializeField] private ImageSpriteTo _BGSpriteOnDown;
        [SerializeField] private ImageSpriteTo _BGSpriteOnEnter;
        [SerializeField] private ImageSpriteTo _BGSpriteOnExit;
        [SerializeField] private RectTransformScale _BGScaleOnDown;
        [SerializeField] private RectTransformScale _BGScaleOnEnter;
        [SerializeField] private RectTransformScale _BGScaleOnExit;
        [SerializeField] private TMProColorTo _textColorOnDown;
        [SerializeField] private TMProColorTo _textColorOnEnter;
        [SerializeField] private TMProColorTo _textColorOnExit;

        public event Action OnButtonClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            foreach (var clickEvent in _clickEvents)
            {
                clickEvent?.Raise();
            }
            
            OnButtonClick?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            foreach (var downEvent in _downEvents)
            {
                downEvent?.Raise();
            }
            _BGColorOnDown?.PerformAsync(_BGImage);
            _BGSpriteOnDown?.PerformAsync(_BGImage);
            _BGScaleOnDown?.PerformAsync(_BGRectTransform);
            _textColorOnDown?.PerformAsync(_text);
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            foreach (var enterEvent in _enterEvents)
            {
                enterEvent?.Raise();
            }
            _BGColorOnEnter?.PerformAsync(_BGImage);
            _BGSpriteOnEnter?.PerformAsync(_BGImage);
            _BGScaleOnEnter?.PerformAsync(_BGRectTransform);
            _textColorOnEnter?.PerformAsync(_text);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            foreach (var exitEvent in _exitEvents)
            {
                exitEvent?.Raise();
            }
            _BGColorOnExit?.PerformAsync(_BGImage);
            _BGSpriteOnExit?.PerformAsync(_BGImage);
            _BGScaleOnExit?.PerformAsync(_BGRectTransform);
            _textColorOnExit?.PerformAsync(_text);
        }
    }
}
