using System;
using System.Net.Mime;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Minesweeper.UI
{
    public class StatTriggerAreaController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool UseThisText => _useThisText;

        public string Text
        {
            get { return _text; }
            set
            {
                if (!_useThisText)
                    _text = value;
                else
                    Debug.LogError("Not supposed to edit this text.");
            }
        }

        public Vector2 AnchorPosition => _anchor.position;
        public TextBubbleAnchorOption AnchorOption => _anchorOption;

        [CanBeNull] public event Action<StatTriggerAreaController> MouseEntered;
        public event Action MouseExited;

        [SerializeField] private bool _useThisText;
        [TextArea, Tooltip("Try to be concise with words and write just one sentence.")]
        [SerializeField] private string _text = "";
        [SerializeField] private Transform _anchor;
        [SerializeField] private TextBubbleAnchorOption _anchorOption;

        private void OnValidate()
        {
            _text = _text?.Trim(' ');
        }

        public void OnPointerEnter(PointerEventData _) => MouseEntered?.Invoke(this);

        public void OnPointerExit(PointerEventData _) => MouseExited?.Invoke();
    }

    public enum TextBubbleAnchorOption
    {
        Upwards,
        Downwards
    }
}