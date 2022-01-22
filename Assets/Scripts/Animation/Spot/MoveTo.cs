using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Move To", menuName = "3D Minesweeper/Animation/Spot/Move To")]
    public class MoveTo : ASpotAnimation
    {
        [SerializeField, Range(0f, 5f)] private float _duration;
        [SerializeField] private Ease _inEase;
        [SerializeField] private Ease _outEase;


        public override async Task PerformAsync(Transform controller, Vector3 position, Action onEnter = null, Action _ = null, Action onExit = null)
        {
            onEnter?.Invoke();

            Vector3 midPoint = (controller.position + position) * 0.5f;
            await controller.DOMove(midPoint, 0.5f * _duration).SetEase(_inEase).AsyncWaitForCompletion();
            await controller.DOMove(position, 0.5f * _duration).SetEase(_outEase).AsyncWaitForCompletion();

            onExit?.Invoke();
            
            await Task.Yield();
        }
        public override async Task PerformAsync(Transform controller, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
            throw new NotSupportedException();
        }
    }
}
