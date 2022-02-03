using UnityEngine;
using Minesweeper.Reference;

namespace Minesweeper.Animation
{
    public class CameraShakeController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraShakeAnimation _bigShake;
        [SerializeField] private CameraShakeAnimation _smallShake;
        [SerializeField] private IntRef _bigThreshold;
        [SerializeField] private IntRef _smallThreshold;

        public async void OnCombo(int combo)
        {
            if (combo > _bigThreshold.value)
            {
                await _bigShake.PerformAsync(_camera);
            }
            else if (combo > _smallThreshold.value)
            {
                await _smallShake.PerformAsync(_camera);
            }
        }
    }
}
