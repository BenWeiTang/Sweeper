using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Bounce All", menuName = "3D Minesweeper/Animation/Grid/Bounce All")]
    public class BounceAll : ASerializedTargetAnimation<IEnumerable<Transform>>
    {
        [SerializeField, Range(0f, 1f)] private float _inDuration;
        [SerializeField, Range(0f, 1f)] private float _outDuration;
        [SerializeField, Range(-0.5f, 0.5f)] private float _delta;
        [SerializeField] private Ease _inEase;
        [SerializeField] private Ease _outEase;

        public override async Task PerformAsync(IEnumerable<Transform> controllers, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            List<Task> firstBounceTasks = new List<Task>();
            List<Task> secondBounceTasks = new List<Task>();
            float ogScaleFactor = controllers.FirstOrDefault().localScale.x;
            float endValue = ogScaleFactor + _delta;
            
            onEnter?.Invoke();
            foreach(var controller in controllers)
            {
                var task = controller.DOScale(endValue, _inDuration).SetEase(_inEase).AsyncWaitForCompletion();
                firstBounceTasks.Add(task);
            }
            await Task.WhenAll(firstBounceTasks);
            onPeak?.Invoke();
            foreach(var controller in controllers)
            {
                var task = controller.DOScale(ogScaleFactor, _outDuration).SetEase(_outEase).AsyncWaitForCompletion();
                secondBounceTasks.Add(task);
            }
            await Task.WhenAll(secondBounceTasks);
            onExit?.Invoke();
        }
    }
}