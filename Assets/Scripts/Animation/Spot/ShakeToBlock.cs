using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Shake To Block", menuName = "3D Minesweeper/Animation/Spot/Shake To Block")]
    public class ShakeToBlock : ASpotAnimation
    {
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _strength;
        [SerializeField] private int _vibrato;
        [SerializeField] private float _randomness;
        [SerializeField] private bool _fadeOut;


        // Does not provide optional Actions for we want to make sure caller implement SwitchToBlock at onPeak
        public override async Task PerformAsync(Transform controller, Action onEnter, Action onPeak, Action onExit)
        {
            onEnter?.Invoke();

            var firstShake = controller.DOShakeRotation(_duration * 0.5f, _strength, 10, _randomness, _fadeOut).AsyncWaitForCompletion();
            while (!firstShake.IsCompleted)
            {
                await Task.Yield();
            }

            onPeak?.Invoke();

            var secondShake = controller.DOShakeRotation(_duration * 0.5f, _strength, 10, _randomness, _fadeOut).AsyncWaitForCompletion();
            while (!secondShake.IsCompleted)
            {
                await Task.Yield();
            }

            onExit?.Invoke();
        }
    }
}
