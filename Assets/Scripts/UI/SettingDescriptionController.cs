using System;
using Minesweeper.Animation;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Minesweeper.UI
{
    public class SettingDescriptionController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _tmpText;
        [SerializeField] private string _text;
        [SerializeField] private CanvasGroupFade _fadeInAnimation;
        [SerializeField] private CanvasGroupFade _fadeOutAnimation;

        public async void OnPointerEnter(PointerEventData eventData)
        {
            await _fadeInAnimation.PerformAsync(_canvasGroup);
        }

        public async void OnPointerExit(PointerEventData eventData)
        {
            await _fadeOutAnimation.PerformAsync(_canvasGroup);
        }

        private void Start()
        {
            _canvasGroup.alpha = 0;
            _tmpText.text = _text.Trim(' ');
        }
    }
}
