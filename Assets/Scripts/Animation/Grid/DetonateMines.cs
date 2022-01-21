using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Detonate Mines", menuName = "3D Minesweeper/Animation/Grid/Detonate Mines")]
    public class DetonateMines : AGridAnimation<Rigidbody>
    {
        [SerializeField, Range(0f, 50f)] private float _explosionForce;
        [SerializeField, Range(0f, 10f)] private float _randomSphereRadius;
        [SerializeField, Range(0f, 20f)] private float _explosionRadius;
        [SerializeField, Range(0f, 20f)] private float _upwardsModifier;
        [SerializeField, Range(0, 100)] private int _delayMinInMS;
        [SerializeField, Range(0, 100)] private int _delayMaxInMS;

        public override async Task PerformAsync(IEnumerable<Rigidbody> rbs, Action onEnter = null, Action onEach = null, Action onExit = null)
        {
            onEnter?.Invoke();

            foreach (var rb in rbs)
            {
                rb.AddExplosionForce(
                    _explosionForce,
                    rb.transform.position + UnityEngine.Random.insideUnitSphere * _randomSphereRadius,
                    _explosionRadius,
                    _upwardsModifier, ForceMode.Impulse
                );

                await Task.Delay(UnityEngine.Random.Range(_delayMinInMS, _delayMaxInMS));
            }
        }
    }
}
