using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Minesweeper.Reference;
using DG.Tweening;

namespace Minesweeper.Animation
{
    public class StartScenePPController : MonoBehaviour
    {
        [SerializeField] private Volume _volume;
        [SerializeField] private BoolRef _firstSession;

        private DepthOfField _dof;
        private Vignette _vignette;
        private float _ogDofValue;
        private float _ogVignetteValue;

        public void OnFirstClicked()
        {
            DOTween.To(() => _dof.focalLength.value, x => _dof.focalLength.value = x, 50f, 1f);
            DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, 0f, 1f);
        }

        private void Awake()
        {
            _volume.profile.TryGet<DepthOfField>(out _dof);
            _volume.profile.TryGet<Vignette>(out _vignette);
            _ogDofValue = _dof.focalLength.value;
            _ogVignetteValue = _vignette.intensity.value;

            // Not very clean code; fix if got time
            if (!_firstSession.value)
            {
                _dof.focalLength.value = 50f;
                _vignette.intensity.value = 0f;
            }
        }

        private void OnDisable()
        {
            _dof.focalLength.value = _ogDofValue;
            _vignette.intensity.value = _ogVignetteValue;
        }
    }
}
