using System.Diagnostics;
using UnityEngine;
using Minesweeper.Reference;

namespace Minesweeper.Core
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private FloatRef _time;

        private Stopwatch _stopwatch;

        private void Awake() => _stopwatch = new Stopwatch();
        
        public void OnFirstSafeSpotDug() => _stopwatch.Restart();
        public void OnGamePaused() => _stopwatch.Stop(); 
        public void OnGameResumed() => _stopwatch.Start();
        public void OnGameFinished()
        {
            _stopwatch.Stop();
            _time.value = (float) _stopwatch.Elapsed.TotalSeconds;
        }
    }
}
