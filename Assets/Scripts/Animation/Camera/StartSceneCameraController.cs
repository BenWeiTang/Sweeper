using UnityEngine;
using Minesweeper.Reference;

namespace Minesweeper.Animation
{
    public class StartSceneCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _camera;

        [Header("Waypoints")]
        [SerializeField] private Transform _origin;
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _settings;

        [Header("Animation")]
        [SerializeField] private MoveTo _moveToAnim;

        [Header("Type Reference")]
        [SerializeField] BoolRef _firstEnterSession;

        private void Start()
        {
            if (_firstEnterSession.value)
            {
                _firstEnterSession.value = false;
                transform.position = _origin.position;
            }
            else
            {
                transform.position = _start.position;
            }
        }

        public async void OnAnyButtonClicked()
        {
            await _moveToAnim.PerformAsync(_camera, _start.position);
        }

        public async void OnSettingsClicked()
        {
            await _moveToAnim.PerformAsync(_camera, _settings.position);
        }

        public async void OnBackToStartClicked()
        {
            await _moveToAnim.PerformAsync(_camera, _start.position);
        }

        private void OnApplicationQuit()
        {
            _firstEnterSession.value = true;
        }
    }
}
