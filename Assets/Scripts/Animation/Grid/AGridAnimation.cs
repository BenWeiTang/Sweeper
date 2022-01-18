using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Minesweeper.Animation
{
    public abstract class AGridAnimation : ScriptableObject
    {
        public virtual async Task PerformAsync(Transform[] controllers,
            Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
        }
    }
}
