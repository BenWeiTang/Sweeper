using UnityEngine;
using Minesweeper.Reference;

namespace Minesweeper.Animation
{
    public class StartSceneCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _origin;
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _settings;

        [SerializeField] private MoveTo _moveToAnim;

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
    }
}
