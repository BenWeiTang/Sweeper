using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Move All To", menuName = "3D Minesweeper/Animation/Grid/Move All To")]
    public class MoveAllTo : AGridAnimation<Transform>
    {
        [SerializeField, Range(0f, 5f)] private float _minMoveTime;
        [SerializeField, Range(0f, 5f)] private float _maxMoveTime;
        [SerializeField] private Ease _easeMode;

        public override async Task PerformAsync(IEnumerable<Transform> controllers, IEnumerable<Vector3> positions, Action onEnter = null, Action onEach = null, Action onExit = null)
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < controllers.Count(); i++)
            {
                Transform current = controllers.ElementAt(i);
                var task = current.DOMove(positions.ElementAt(i), UnityEngine.Random.Range(_minMoveTime, _maxMoveTime))
                        .SetEase(_easeMode)
                        .AsyncWaitForCompletion();
                tasks.Add(task);
            }
            await Task.WhenAll(tasks);
        }
    }
}
