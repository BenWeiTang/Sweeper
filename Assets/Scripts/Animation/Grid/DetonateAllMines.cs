using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Detonate All Mines", menuName = "3D Minesweeper/Animation/Grid/Detonate All Mines")]
    public class DetonateAllMines : AGridAnimation
    {
        [SerializeField, Range(0f, 50f)] private float _explosionForce;
        [SerializeField, Range(0f, 10f)] private float _randomSphereRadius;
        [SerializeField, Range(0f, 20f)] private float _explosionRadius;
        [SerializeField, Range(0f, 20f)] private float _upwardsModifier;
        [SerializeField] private bool _detonateMarkedMines;

        public override async Task PerformAsync(Transform[] _, Rigidbody[] rbs, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            if (_detonateMarkedMines)
            {

            }
            else
            {

            }
            await Task.Yield();
        }
    }
}
