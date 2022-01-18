using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Float All", menuName = "3D Minesweeper/Animation/Grid/Float All")]
    public class FloatAll : AGridAnimation
    {
        [SerializeField, Range(0f, 2.5f)] private float _initForce;
        [SerializeField, Range(0f, 2.5f)] private float _initTorque;

        public override async Task PerformAsync(IEnumerable<Transform> _, IEnumerable<Rigidbody> rigidbodies, Action __, Action ___, Action ____)
        {
            foreach (var rb in rigidbodies)
            {
                Vector3 rndDir = UnityEngine.Random.onUnitSphere;
                Vector3 rndTorque = UnityEngine.Random.onUnitSphere;
                rb.AddForce(rndDir * _initForce, ForceMode.Impulse);
                rb.AddTorque(rndTorque * _initTorque, ForceMode.Impulse);
            }

            await Task.Yield();
        }
    }
}
