using UnityEngine;
using TMPro;
using Minesweeper.Reference;

namespace Minesweeper.UI.PanelController
{
    public class WinPanelController : APanelController
    {
        [SerializeField] private TextMeshProUGUI _clickText;
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _effText;
        [SerializeField] private IntRef _clickCount;
        [SerializeField] private FloatRef _time;
        [SerializeField] private IntRef _safeSpotCount;
        public override PanelType Type => PanelType.Win;

        private void OnEnable()
        {
            _clickText.text = "Clicks: " + _clickCount.value;
            _timeText.text = "Time: " + _time.value.ToString("n2") + " s";
            float efficiency = (float)((double)_safeSpotCount.value/(double)_clickCount.value) * 100;
            _effText.text = "Efficiency: " + efficiency.ToString("n2") + "%";
        }
    }
}
