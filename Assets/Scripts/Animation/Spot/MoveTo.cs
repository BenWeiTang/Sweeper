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


        ///<summary>
        ///Moves the controller to the position
        ///</summary>
        ///<param name = "controller">The controller to move</param>
        ///<param name = "position">The target posistion</param>
        public override async Task PerformAsync(Transform controller, Vector3 position, Action onEnter = null, Action _ = null, Action onExit = null)
        {
            onEnter?.Invoke();

            Vector3 midPoint = (controller.position + position) * 0.5f;
            await controller.DOMove(midPoint, 0.5f * _duration).SetEase(_inEase).AsyncWaitForCompletion();
            await controller.DOMove(position, 0.5f * _duration).SetEase(_outEase).AsyncWaitForCompletion();

            onExit?.Invoke();
            
            await Task.Yield();
        }
    }
}
