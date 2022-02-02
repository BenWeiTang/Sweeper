using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper.Animation
{
    public abstract class AImageAnimation : AUIAnimation<Image>
    {
        public override async Task PerformAsync(Image image,
            Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
        }
    }
}
