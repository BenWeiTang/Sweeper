using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Bounce", menuName = "3D Minesweeper/Animation/Spot/Bounce")]
    public class Bounce : ASpotAnimation
    {
        [SerializeField] private float _inDuration;
        [SerializeField] private float _outDuration;
        [SerializeField] private float _delta;
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
    }
}
