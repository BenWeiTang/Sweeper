using System.Collections;
using UnityEngine;
using Minesweeper.Event;

namespace Minesweeper.Core
{
    public class ComboController : MonoBehaviour
    {
        [SerializeField] private IntEvent ManySafeSpotsDig;

        private int _combo = 0;

        public void StartCountingDigs() => StartCoroutine(CountDigs());
        public void OnSafeSpotDug(int _) => _combo++;

        private IEnumerator CountDigs()
        {
            yield return null;
            ManySafeSpotsDig.Raise(_combo);
            _combo = 0;
        }
    }
}
