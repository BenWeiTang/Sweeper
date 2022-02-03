using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Minesweeper.Event;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Detonate Mines", menuName = "3D Minesweeper/Animation/Grid/Detonate Mines")]
    public class DetonateMines : ASerializedTargetAnimation<IEnumerable<Rigidbody>>
    {
        [SerializeField, Range(0f, 50f)] private float _explosionForce;
        [SerializeField, Range(0f, 10f)] private float _randomSphereRadius;
        [SerializeField, Range(0f, 20f)] private float _explosionRadius;
        [SerializeField, Range(0f, 20f)] private float _upwardsModifier;
        [SerializeField, Range(0, 100)] private int _delayMinInMS;
        [SerializeField, Range(0, 100)] private int _delayMaxInMS;
        [SerializeField] private VoidEvent MineDetonate;

        public override async Task PerformAsync(IEnumerable<Rigidbody> rbs, Action onEnter = null, Action onEach = null, Action onExit = null)
        {
            onEnter?.Invoke();

            foreach (var rb in rbs)
            {
                onEach?.Invoke();
                rb.AddExplosionForce(
                    _explosionForce,
                    rb.transform.position + UnityEngine.Random.insideUnitSphere * _randomSphereRadius,
                    _explosionRadius,
                    _upwardsModifier, ForceMode.Impulse
                );

                MineDetonate.Raise();
                await Task.Delay(UnityEngine.Random.Range(_delayMinInMS, _delayMaxInMS));
            }

            onExit?.Invoke();
        }
    }
}
