using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Rotate All Towards", menuName = "3D Minesweeper/Animation/Grid/Rotate All Towards")]
    public class RotateAllTowards : ADynamicTargetAnimation<IEnumerable<Transform>, Vector3>
    {
        [SerializeField, Range(0f, 2f)] private float _minRotateTime;
        [SerializeField, Range(0f, 2f)] private float _maxRotateTime;
        [SerializeField] private Ease _easeMode;
        public override async Task PerformAsync(IEnumerable<Transform> controllers, Vector3 rotation, Action onEnter = null, Action onEach = null, Action onExit = null)
        {
            await Task.Yield();
            var rotationTasks = new List<Task>();
            onEnter?.Invoke();
            foreach (var current in controllers)
            {
                var task = current.DORotate(rotation, UnityEngine.Random.Range(_minRotateTime, _maxRotateTime))
                    .SetEase(_easeMode)
                    .AsyncWaitForCompletion();
                rotationTasks.Add(task);
            }
            await Task.WhenAll(rotationTasks);
            onExit?.Invoke();
        }
    }
}
