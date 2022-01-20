using UnityEngine;
using Minesweeper.Reference;

namespace Minesweeper.Core
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private FloatRef _time;

        private float _start = 0f;

        public void OnFirstSafeSpotDug()
        {
            _start = Time.time;
        }
        
        public void OnGameFinished()
        {
            _time.value = Time.time - _start;
        }
    }
}
