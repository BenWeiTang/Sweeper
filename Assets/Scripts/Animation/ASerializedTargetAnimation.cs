using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Minesweeper.Animation
{
    public abstract class ASerializedTargetAnimation<T> : ScriptableObject
    {
        public virtual async Task PerformAsync(T item, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
        }
    }
}
