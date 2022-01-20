using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Minesweeper.Animation
{
    public abstract class ACanvasGroupAnimation : ScriptableObject
    {
        public virtual async Task PerformAsync(CanvasGroup canvasGroup,
            Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
        }
    }
}
