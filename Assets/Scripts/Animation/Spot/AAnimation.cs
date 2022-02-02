using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Minesweeper.Animation
{
    public abstract class AAnimation<T> : ScriptableObject
    {
        public virtual async Task PerformAsync(T item, Action onEnter, Action onPeak, Action onExit)
        {
            await Task.Yield();
        }
    }
}
