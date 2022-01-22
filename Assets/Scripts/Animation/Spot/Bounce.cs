using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Bounce", menuName = "3D Minesweeper/Animation/Spot/Bounce")]
    public class Bounce : ASpotAnimation
    {
        [SerializeField, Range(0f, 0.1f)] private float _inDuration;
        [SerializeField, Range(0f, 0.2f)] private float _outDuration;
        [SerializeField, Range(-0.5f, 0.5f)] private float _delta;
        [SerializeField] private Ease _inEase;
        [SerializeField] private Ease _outEase;


        public override async Task PerformAsync(Transform controller, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            Sequence s = DOTween.Sequence();
            float ogScaleFactor = controller.localScale.x;
            float endValue = ogScaleFactor + _delta;

            onEnter?.Invoke();
            await controller.DOScale(endValue, _inDuration).SetEase(_inEase).AsyncWaitForCompletion();
            onPeak?.Invoke();
            await controller.DOScale(ogScaleFactor, _outDuration).SetEase(_outEase).AsyncWaitForCompletion();
            onExit?.Invoke();
        }

        public override async Task PerformAsync(Transform controller, Vector3 _, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
            throw new NotSupportedException();
        }
    }
}