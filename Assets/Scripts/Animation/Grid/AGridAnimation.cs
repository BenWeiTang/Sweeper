using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Minesweeper.Animation
{
    public abstract class AGridAnimation : ScriptableObject
    {
        public virtual async Task PerformAsync(IEnumerable<Rigidbody> rigidbodies,
            Action onEnter = null, Action onEach = null, Action onExit = null)
        {
            await Task.Yield();
        }

        public virtual async Task PerformAsync(IEnumerable<Rigidbody> rigidbodies, IEnumerable<Vector3> positions,
            Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
        }

        public virtual async Task PerformAsync(IEnumerable<Rigidbody> rigidbodies, Vector3 positions,
            Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
        }

        public virtual async Task PerformAsync(IEnumerable<Transform> controllers,
            Action onEnter = null, Action onEach = null, Action onExit = null)
        {
            await Task.Yield();
        }

        public virtual async Task PerformAsync(IEnumerable<Transform> controllers, IEnumerable<Vector3> positions,
            Action onEnter = null, Action onEach = null, Action onExit = null)
        {
            await Task.Yield();
        }

        public virtual async Task PerformAsync(IEnumerable<Transform> controllers, Vector3 position,
            Action onEnter = null, Action onEach = null, Action onExit = null)
        {
            await Task.Yield();
        }
    }
}