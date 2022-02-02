using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Minesweeper.Animation
{
    public abstract class ADynamicTargetAnimation<T1, T2> : ScriptableObject
    {
        public virtual async Task PerformAsync(T1 item, T2 target, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
        }
    }
}
