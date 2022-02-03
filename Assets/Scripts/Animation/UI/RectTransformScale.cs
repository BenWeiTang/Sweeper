using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "RectTransform Scale", menuName = "3D Minesweeper/Animation/UI/RectTransform Scale")]
    public class RectTransformScale : ASerializedTargetAnimation<RectTransform>
    {
        [SerializeField] private Vector3 _targetLocalScale;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _inEaseMode;
        [SerializeField] private Ease _outEaseMode;

        public override async Task PerformAsync(RectTransform item, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            Vector2 midpoint = (_targetLocalScale - item.localScale) * 0.5f;
            onEnter?.Invoke();
            await item.DOScale(midpoint, _duration * 0.5f).SetEase(_inEaseMode).AsyncWaitForCompletion();
            onPeak?.Invoke();
            await item.DOScale(_targetLocalScale, _duration * 0.5f).SetEase(_outEaseMode).AsyncWaitForCompletion();
            onExit?.Invoke();
        }
    }
}
