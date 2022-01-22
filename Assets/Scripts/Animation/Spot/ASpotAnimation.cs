using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Minesweeper.Animation
{
    public abstract class ASpotAnimation : ScriptableObject
    {
        public virtual async Task PerformAsync(Transform controller, 
            Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
        }

        public virtual async Task PerformAsync(Transform controller,
            Vector3 position, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
                await Task.Yield();
        }
    }
}