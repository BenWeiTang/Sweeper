using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "RectTransform To", menuName = "3D Minesweeper/Animation/UI/RectTransform To")]
    public class RectTransformTo : ASerializedTargetAnimation<RectTransform>
    {
        [SerializeField] private Vector2 _targetAnchorPosition;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _inEaseMode;
        [SerializeField] private Ease _outEaseMode;

        public override async Task PerformAsync(RectTransform item, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            Vector2 midpoint = (_targetAnchorPosition - item.anchoredPosition) * 0.5f;
            onEnter?.Invoke();
            await item.DOAnchorPos(midpoint, _duration * 0.5f).SetEase(_inEaseMode).AsyncWaitForCompletion();
            onPeak?.Invoke();
            await item.DOAnchorPos(_targetAnchorPosition, _duration * 0.5f).SetEase(_outEaseMode).AsyncWaitForCompletion();
            onExit?.Invoke();
        }
    }
}
