using UnityEngine;
using TMPro;
using Minesweeper.Reference;

namespace Minesweeper.UI
{
    public class WinPanelController : APanelController
    {
        [SerializeField] private TextMeshProUGUI _timeText;
        // [SerializeField] private TextMeshProUGUI _timeDetailText;
        [SerializeField] private TextMeshProUGUI _clickText;
        // [SerializeField] private TextMeshProUGUI _clickDetailText;
        [SerializeField] private TextMeshProUGUI _effText;
        // [SerializeField] private TextMeshProUGUI _effDetailText;
        [SerializeField] private TextMeshProUGUI _ACEText;
        // [SerializeField] private TextMeshProUGUI _ACEDetailText;
        [SerializeField] private IntRef _clickCount;
        [SerializeField] private FloatRef _time;
        [SerializeField] private IntRef _safeSpotCount;
        [SerializeField] private FloatRef _ACE;
        public override PanelType Type => PanelType.Win;

        private void OnEnable()
        {
            UpdateClickText();
            UpdateTimeText();
            UpdateEffText();
            UpdateACEText();
        }

        private void UpdateClickText()
        {
            _clickText.text = _clickCount.value.ToString();
            // _clickDetailText.text = _clickCount.value.ToString();
        }

        private void UpdateTimeText()
        {
            _timeText.text = Mathf.RoundToInt(_time.value).ToString() + "s";
            // _timeDetailText.text = _time.value.ToString("n2") + "s";
        }

        private void UpdateEffText()
        {
            float efficiency = (float)((double)_safeSpotCount.value / (double)_clickCount.value) * 100;
            _effText.text = Mathf.RoundToInt(efficiency).ToString() + "%";
            // _effDetailText.text = efficiency.ToString("n2") + "%";
        }

        private void UpdateACEText()
        {
            float ace = _ACE.value * 100f;
            _ACEText.text = Mathf.RoundToInt(ace) + "%";
            // _ACEDetailText.text = ace.ToString("n2") + "%";
        }
    }
}
