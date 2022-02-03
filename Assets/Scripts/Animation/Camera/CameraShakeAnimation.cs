using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "New Camera Shake", menuName = "3D Minesweeper/Animation/Camera/Shake")]
    public class CameraShakeAnimation : ASerializedTargetAnimation<Camera>
    {
        [SerializeField] private float _duration;
        [SerializeField] private Vector3 _strength;
        [SerializeField] private int _vibrato;
        [SerializeField] private float _randomness;
        [SerializeField] private bool _fadeOut;

        public override async Task PerformAsync(Camera camera, Action onEnter = null, Action _ = null, Action onExit = null)
        {
            onEnter?.Invoke();
            await camera.DOShakePosition(_duration, _strength, _vibrato, _randomness, _fadeOut).AsyncWaitForCompletion();
            onExit?.Invoke();
        }
    }
}
