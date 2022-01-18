using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Bounce", menuName = "3D Minesweeper/Animation/Spot/Bounce")]
    public class BounceAll : AGridAnimation
    {
        public override async Task PerformAsync(Transform[] controllers, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
        }
    }
}