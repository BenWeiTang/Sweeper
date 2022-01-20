using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Animation
{

    [CreateAssetMenu(fileName = "Canvas Group Fade", menuName = "3D Minesweeper/Animation/UI/Canvas Group Fade")]
    public class CanvasGroupFade : ACanvasGroupAnimation
    {
        [SerializeField] private bool _toFadeIn;
        [SerializeField, Range(0f, 2f)] private float _fadeDuration;
        [SerializeField] private Ease _easeMode;
        public override async Task PerformAsync(CanvasGroup canvasGroup, Action onEnter = null, Action _ = null, Action onExit = null)
        {
            onEnter?.Invoke();
            await canvasGroup.DOFade(_toFadeIn ? 1f : 0f, _fadeDuration)
                .SetEase(_easeMode)
                .AsyncWaitForCompletion();
            onExit?.Invoke();
        }
    }
}
