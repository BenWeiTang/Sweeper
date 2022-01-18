using System;
using System.Collections.Generic;
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
        [SerializeField, Range(0, 100)] private int _delayMinInMS;
        [SerializeField, Range(0, 100)] private int _delayMaxInMS;
        [SerializeField] private bool _detonateMarkedMines;

        public override async Task PerformAsync(IEnumerable<Transform> _, IEnumerable<Rigidbody> rbs, Action onEnter, Action __, Action ___)
        {
            onEnter?.Invoke();
            if (_detonateMarkedMines)
            {
                foreach (var rb in rbs)
                {
                    rb.AddExplosionForce(
                        _explosionForce, 
                        rb.transform.position + UnityEngine.Random.insideUnitSphere * _randomSphereRadius, 
                        _explosionRadius, 
                        _upwardsModifier, ForceMode.Impulse);
                    
                    await Task.Delay(UnityEngine.Random.Range(_delayMinInMS, _delayMaxInMS));
                }
            }
            else
            {

            }
        }
    }
}
