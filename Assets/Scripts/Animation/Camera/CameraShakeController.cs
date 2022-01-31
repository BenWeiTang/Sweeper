using UnityEngine;

namespace Minesweeper.Animation
{
    public class CameraShakeController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private CameraShakeAnimation _bigShake;
        [SerializeField] private CameraShakeAnimation _smallShake;

        public async void OnCombo(int combo)
        {
            if (combo > 50)
            {
                await _bigShake.PerformAsync(_camera);
            }
            else if (combo > 25)
            {
                await _smallShake.PerformAsync(_camera);
            }
        }
    }
}
