using UnityEngine;
using TMPro;
using Minesweeper.Reference;

namespace Minesweeper.UI
{
    public class WinPanelController : APanelController
    {
        public override PanelType Type => PanelType.Win;

        [Header("Text Component")]
        [SerializeField] private TextMeshProUGUI _timeText;
        [SerializeField] private TextMeshProUGUI _clickText;
        [SerializeField] private TextMeshProUGUI _effText;
        [SerializeField] private TextMeshProUGUI _ACEText;

        [Header("Trigger Area")]
        [SerializeField] private TextMeshProUGUI _textBubble;
        [SerializeField] private RectTransform _textBubbleTransform;
        [SerializeField] private RectTransform _textBubbleBg;
        [SerializeField] private StatTriggerAreaController _timeName;
        [SerializeField] private StatTriggerAreaController _timeNumber;
        [SerializeField] private StatTriggerAreaController _clickName;
        [SerializeField] private StatTriggerAreaController _clickNumber;
        [SerializeField] private StatTriggerAreaController _effName;
        [SerializeField] private StatTriggerAreaController _effNumber;
        [SerializeField] private StatTriggerAreaController _ACEName;
        [SerializeField] private StatTriggerAreaController _ACENumber;

        [Header("Reference")]
        [SerializeField] private FloatRef _time;
        [SerializeField] private IntRef _clickCount;
        [SerializeField] private IntRef _safeSpotCount;
        [SerializeField] private FloatRef _ACE;


        #region UNITY_METHODS
        
        private void OnEnable()
        {
            UpdateTimeText();
            UpdateClickText();
            UpdateEffText();
            UpdateACEText();
            UpdateRegistration(true);
        }

        private void OnDisable() => UpdateRegistration(false);
        
        #endregion

        #region PRIVATE_METHODS
        
        private void UpdateTimeText()
        {
            _timeText.text = Mathf.RoundToInt(_time.value).ToString() + "s";
            _timeNumber.Text = _time.value.ToString("n2") + "s";
        }
        
        private void UpdateClickText()
        {
            _clickText.text = _clickCount.value.ToString();
            _clickNumber.Text = _clickCount.value.ToString();
        }

        private void UpdateEffText()
        {
            float efficiency = (float) ((double) _safeSpotCount.value / (double) _clickCount.value) * 100;
            _effText.text = Mathf.RoundToInt(efficiency).ToString() + "%";
            _effNumber.Text = efficiency.ToString("n2") + "%";
        }

        private void UpdateACEText()
        {
            float ace = _ACE.value * 100f;
            _ACEText.text = Mathf.RoundToInt(ace) + "%";
            _ACENumber.Text = ace.ToString("n2") + "%";
        }

        private void UpdateRegistration(bool toRegister)
        {
            if (toRegister)
            {
                // Mouse Entered
                _timeName.MouseEntered += MoveTextBubble;
                _timeNumber.MouseEntered += MoveTextBubble;
                _clickName.MouseEntered += MoveTextBubble;
                _clickNumber.MouseEntered += MoveTextBubble;
                _effName.MouseEntered += MoveTextBubble;
                _effNumber.MouseEntered += MoveTextBubble;
                _ACEName.MouseEntered += MoveTextBubble;
                _ACENumber.MouseEntered += MoveTextBubble;
                // Mouse Exited
                _timeName.MouseExited += HideTextBubble;
                _timeNumber.MouseExited += HideTextBubble;
                _clickName.MouseExited += HideTextBubble;
                _clickNumber.MouseExited += HideTextBubble;
                _effName.MouseExited += HideTextBubble;
                _effNumber.MouseExited += HideTextBubble;
                _ACEName.MouseExited += HideTextBubble;
                _ACENumber.MouseExited += HideTextBubble;
            }
            else
            {
                // Mouse Entered
                _timeName.MouseEntered -= MoveTextBubble;
                _timeNumber.MouseEntered -= MoveTextBubble;
                _clickName.MouseEntered -= MoveTextBubble;
                _clickNumber.MouseEntered -= MoveTextBubble;
                _effName.MouseEntered -= MoveTextBubble;
                _effNumber.MouseEntered -= MoveTextBubble;
                _ACEName.MouseEntered -= MoveTextBubble;
                _ACENumber.MouseEntered -= MoveTextBubble;
                // Mouse Exited
                _timeName.MouseExited -= HideTextBubble;
                _timeNumber.MouseExited -= HideTextBubble;
                _clickName.MouseExited -= HideTextBubble;
                _clickNumber.MouseExited -= HideTextBubble;
                _effName.MouseExited -= HideTextBubble;
                _effNumber.MouseExited -= HideTextBubble;
                _ACEName.MouseExited -= HideTextBubble;
                _ACENumber.MouseExited -= HideTextBubble;
            }
        }

        private void MoveTextBubble(StatTriggerAreaController controller)
        {
            bool isUpwards = controller.AnchorOption == TextBubbleAnchorOption.Upwards;
            
            _textBubbleTransform.pivot = new Vector2(0.5f, isUpwards ? 0f : 1f);
            Vector2 offset = isUpwards ? Vector2.up : Vector2.down;
            offset *= 10f;
            _textBubbleTransform.position = controller.AnchorPosition + offset;
            _textBubbleBg.localScale = new Vector3(1f, isUpwards ? 1f : -1f, 1f);

            _textBubble.text = controller.Text;
            
            _textBubbleTransform.gameObject.SetActive(true);
        }

        private void HideTextBubble()
        {
            _textBubbleTransform.gameObject.SetActive(false);
        }

        #endregion
    }
}