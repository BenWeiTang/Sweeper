using System;
using UnityEngine;
using Minesweeper.Reference;
using TMPro;
using Random = UnityEngine.Random;

namespace Minesweeper.UI
{
    public class LosePanelController : APanelController
    {
        public override PanelType Type => PanelType.Lose;

        [SerializeField] private TextMeshProUGUI _quoteText;
        [SerializeField] private StringRef[] _quotes;

        private int _lastIndex = 0;

        private void OnEnable()
        {
            _quoteText.text = GetRandomQuote();
        }

        private string GetRandomQuote()
        {
            int rndIndex;
            do
            {
                rndIndex = Random.Range(0, _quotes.Length);
            } while (_lastIndex == rndIndex);
            
            _lastIndex = rndIndex;
            return _quotes[rndIndex].value;
        }
    }
}