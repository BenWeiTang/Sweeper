using System.Collections.Generic;
using UnityEngine;
using Minesweeper.Reference;

namespace Minesweeper.Core
{
    public class ACEController : MonoBehaviour
    {
        [SerializeField] IntRef _clickCount;
        [SerializeField] IntRef _effective;
        [SerializeField] FloatRef _ACE;

        private HashSet<int> _markedSpotIndices = new HashSet<int>();
        
        public void UpdateClickCount(int delta) => _clickCount.value += delta;

        public void OnCombo(int combo)
        {
            if (combo > 0)
            {
                _effective.value++;
            }
        }

        public void OnSpotMarkedAt(int index)
        {
            if (_markedSpotIndices.Contains(index))
            {
                _effective.value = Mathf.Max(_effective.value - 1, 0);
            }
            else
            {
                _markedSpotIndices.Add(index);
                _effective.value++;
            }
        }

        public void OnMineDugAt(int index) => _effective.value = Mathf.Max(_effective.value - 1, 0);

        public void OnGameReady()
        {
            _clickCount.value = 0;
            _effective.value = 0;
            _ACE.value = 0f;
        }

        public void OnGameFinished(bool _)
        {
            _ACE.value = (float)_effective.value / _clickCount.value;
        }

        private void OnDisable()
        {
            _effective.value = 0;
            _markedSpotIndices.Clear();
        }
    }
}
