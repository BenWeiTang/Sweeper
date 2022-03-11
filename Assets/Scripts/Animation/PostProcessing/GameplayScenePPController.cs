using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Minesweeper.Reference;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

namespace Minesweeper.Animation
{
    public class GameplayScenePPController : MonoBehaviour
    {
        [SerializeField] private Volume _volume;
        [Header("Depth of Field")]
        [SerializeField] private float _dofTargetValue = 50f;
        [Header("Impact-Related")]
        [SerializeField, Range(0f, 1f)] private float _vignetteMin;
        [SerializeField, Range(0f, 1f)] private float _vignetteMax;
        [SerializeField, Range(0f, 1f), Tooltip("CA stands for Chromatic Aberration")] private float _CATargetValue;
        [SerializeField] private IntRef _bigCameraShakeThreshold;
        [SerializeField] private IntRef _smallCameraShakeThreshold;
        [SerializeField] private float _bigComboRecoveryTime;
        [SerializeField] private float _smallComboRecoveryTime;


        private DepthOfField _dof;
        private Vignette _vignette;
        private ChromaticAberration _chromaticAberration;

        private float _dofOGValue;
        private float _vignetteCurrent;
        private TweenerCore<float, float, FloatOptions> _vignetteAnimation;

        public async void OnGamePaused()
        {
            DOTween.To(() => _dof.focalLength.value, x => _dof.focalLength.value = x, _dofTargetValue, 0.5f);
            await VignettePause(true);
        }

        public async void OnGameResumed()
        {
            DOTween.To(() => _dof.focalLength.value, x => _dof.focalLength.value = x, _dofOGValue, 0.5f);
            await VignettePause(false);
        }

        public async void OnPostGameExit(bool _)
        {
            DOTween.To(() => _dof.focalLength.value, x => _dof.focalLength.value = x, _dofTargetValue, 0.5f);
            await VignettePause(true);
        }

        public async void OnGameRestarted(bool _)
        {
            DOTween.To(() => _dof.focalLength.value, x => _dof.focalLength.value = x, _dofOGValue, 0.5f);
            await VignettePause(false);
        }

        public async void OnComboHit(int combo)
        {
            if (combo >= _bigCameraShakeThreshold.value)
            {
                // vignette decrease
                // chromatic aberration 
                var tasks = new[]
                {
                    VignetteDecrease(_bigComboRecoveryTime), 
                    TriggerChromaticAberration()
                };
                await Task.WhenAll(tasks);

            }
            else if (combo >= _smallCameraShakeThreshold.value)
            {
                // vignette decrease
                await VignetteDecrease(_smallComboRecoveryTime);
            }
        }

        private void Awake()
        {
            _volume.profile.TryGet<DepthOfField>(out _dof);
            _volume.profile.TryGet<Vignette>(out _vignette);
            _volume.profile.TryGet<ChromaticAberration>(out _chromaticAberration);
            
            _dofOGValue = _dof.focalLength.value;
        }

        private void Start()
        {
            _vignette.intensity.value = _vignetteMin;
            _vignetteAnimation = DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, _vignetteMax, 1f).SetLoops(-1, LoopType.Yoyo);
        }

        private async Task VignetteDecrease(float timeUntilRecovery)
        {
            await VignettePause(true);
            await Task.Delay((int) timeUntilRecovery * 1_000);
            await VignettePause(false);
            _vignetteAnimation.Play();
        }

        private async Task VignettePause(bool toPause)
        {
            if (toPause)
            {
                _vignetteCurrent = _vignette.intensity.value;
                _vignetteAnimation.Pause();
                await DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, 0f, 0.1f).AsyncWaitForCompletion();
            }
            else
            {
                await DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, _vignetteCurrent, 0.5f).AsyncWaitForCompletion();
                _vignetteAnimation.Play();
            }
        }

        private async Task TriggerChromaticAberration()
        {
            await DOTween.To(() => _chromaticAberration.intensity.value, x => _chromaticAberration.intensity.value = x, _CATargetValue, 0.2f)
                .SetEase(Ease.OutBounce).AsyncWaitForCompletion();
            _chromaticAberration.intensity.value = 0f;
        }
    }
}