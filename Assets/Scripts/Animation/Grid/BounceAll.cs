using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Bounce All", menuName = "3D Minesweeper/Animation/Grid/Bounce All")]
    public class BounceAll : AGridAnimation
    {
        [SerializeField, Range(0f, 1f)] private float _inDuration;
        [SerializeField, Range(0f, 1f)] private float _outDuration;
        [SerializeField, Range(-0.5f, 0.5f)] private float _delta;
        [SerializeField] private Ease _inEase;
        [SerializeField] private Ease _outEase;

        public override async Task PerformAsync(Transform[] controllers, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            List<Task> tasks = new List<Task>();
            float ogScaleFactor = controllers[0].localScale.x;
            float endValue = ogScaleFactor + _delta;
            foreach(var controller in controllers)
            {
                Sequence s = DOTween.Sequence();
                s.Append(controller.DOScale(endValue, _inDuration).SetEase(_inEase));
                s.Append(controller.DOScale(ogScaleFactor, _outDuration).SetEase(_outEase));
                var currentTask = s.AsyncWaitForCompletion();
                tasks.Add(currentTask);
            }

            await Task.WhenAll(tasks);
        }
    }
}