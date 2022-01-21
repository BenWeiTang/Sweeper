using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Minesweeper.Animation
{
    public abstract class AGridAnimation<T> : ScriptableObject
    {
        public virtual async Task PerformAsync(IEnumerable<T> rigidbodies,
            Action onEnter = null, Action onEach = null, Action onExit = null)
        {
            await Task.Yield();
        }

        public virtual async Task PerformAsync(IEnumerable<T> rigidbodies, IEnumerable<Vector3> positions,
            Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
        }

        public virtual async Task PerformAsync(IEnumerable<T> rigidbodies, Vector3 positions,
            Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
        }
    }
}