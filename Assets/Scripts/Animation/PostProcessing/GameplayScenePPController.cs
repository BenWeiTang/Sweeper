using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

namespace Minesweeper.Animation
{
    public class GameplayScenePPController : MonoBehaviour
    {
        [SerializeField] private Volume _volume;

        private DepthOfField _dof;
        
        private float _dofOGValue;
        private float _dofTargetValue = 50f;

        public void OnGamePaused()
        {
            DOTween.To(() => _dof.focalLength.value, x => _dof.focalLength.value = x, _dofTargetValue, 0.5f);
        }
        public void OnGameResumed()
        {
            DOTween.To(() => _dof.focalLength.value, x => _dof.focalLength.value = x, _dofOGValue, 0.5f);
        }

        public void OnPostGameExit(bool _)
        {
            DOTween.To(() => _dof.focalLength.value, x => _dof.focalLength.value = x, _dofTargetValue, 0.5f);
        }
        
        public void OnGameRestarted(bool _)
        {
            DOTween.To(() => _dof.focalLength.value, x => _dof.focalLength.value = x, _dofOGValue, 0.5f); 
        }

        private void Awake() 
        {
            _volume.profile.TryGet<DepthOfField>(out _dof);
            _dofOGValue = _dof.focalLength.value;
        }
    }
}
