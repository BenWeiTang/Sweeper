using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Minesweeper.Animation
{
    public abstract class AGridAnimation : ScriptableObject
    {
        public virtual async Task PerformAsync(IEnumerable<Transform> controllers, IEnumerable<Rigidbody> rigidbodies,
            Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
        }
    }
}