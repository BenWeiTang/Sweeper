using UnityEngine;
using TMPro;
using Minesweeper.Reference;
using UnityEngine.UI;

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

        [Header("Text Bubble")]
        [SerializeField] private TextMeshProUGUI _textBubble;
        [SerializeField] private LayoutElement _textBubbleLayoutElement;
        [SerializeField] private int _textWrapLimit;
        [SerializeField] private RectTransform _textBubbleTransform;
        
        [Header("Trigger Area")]
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
            _timeText.text = Mathf.FloorToInt(_time.value).ToString() + "s";
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
            _effText.text = Mathf.FloorToInt(efficiency).ToString() + "%";
            _effNumber.Text = efficiency.ToString("n2") + "%";
        }

        private void UpdateACEText()
        {
            float ace = _ACE.value * 100f;
            _ACEText.text = Mathf.FloorToInt(ace) + "%";
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

            // Positioning text bubble
            _textBubbleTransform.pivot = new Vector2(0.5f, isUpwards ? 0f : 1f);
            // Vector2 offset = isUpwards ? Vector2.up * 5f : Vector2.down * 10f;
            _textBubbleTransform.position = controller.AnchorPosition;//+ offset;

            // Update text content
            _textBubble.text = controller.Text;
            
            // Set active before refresh layout element for auto-wrapping
            _textBubbleTransform.gameObject.SetActive(true);
            _textBubbleLayoutElement.enabled = _textBubble.text.Length > _textWrapLimit;
        }

        private void HideTextBubble()
        {
            _textBubbleTransform.gameObject.SetActive(false);
        }

        #endregion
    }
}