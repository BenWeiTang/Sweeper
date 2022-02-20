using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Minesweeper.Reference;
using Minesweeper.Event;

namespace Minesweeper.Animation
{
    public class StartSceneCameraController : MonoBehaviour
    {
        [SerializeField] private Transform _camera;

        [Header("Event")]
        [SerializeField] private VoidEvent ReadyToQuitGame;

        [Header("Waypoints")]
        [SerializeField] private Transform _origin;
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _settings;
        [SerializeField] private Transform _exit;

        [Header("Animation")]
        [SerializeField] private MoveTo _moveToAnim;
        [SerializeField] private RotateTowards _rotateAnim;
        

        [Header("Type Reference")]
        [SerializeField] private BoolRef _firstEnterSession;


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

        public async void OnExitClicked()
        {
            var tasks = new List<Task>();
            var moveTask = _moveToAnim.PerformAsync(_camera, _exit.position);
            var rotateTask = _rotateAnim.PerformAsync(_camera, new Vector3(0f, -90f, 0f));
            tasks.Add(moveTask);
            tasks.Add(rotateTask);
            await Task.WhenAll(tasks);
            
            ReadyToQuitGame.Raise();
        }

        private void OnApplicationQuit()
        {
            _firstEnterSession.value = true;
        }
    }
}
