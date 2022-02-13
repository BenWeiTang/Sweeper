using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Minesweeper.Animation;
using TMPro;

namespace Minesweeper.UI
{
    public class SettingsPanelController : APanelController
    {
        public override PanelType Type => PanelType.Settings;
        public Tab CurrentTab { get; private set; }

        [Header("Tabs")] [SerializeField] private CanvasGroup _generalTabContent;
        [SerializeField] private CanvasGroup _audioTabContent;
        [SerializeField] private CanvasGroup _customizeTabContent;
        [SerializeField] private ButtonController _generalButton;
        [SerializeField] private ButtonController _audioButton;
        [SerializeField] private ButtonController _customizeButton;

        [Header("Animation")] 
        [SerializeField] private CanvasGroupFade _tabFadeInAnim;
        [SerializeField] private CanvasGroupFade _tabFadeOutAnim;
        [SerializeField] private TMProColorTo _colorToInAnim;
        [SerializeField] private TMProColorTo _colorToOutAnim;

        private Tab _generalTab;
        private Tab _audioTab;
        private Tab _customizeTab;

        private void Awake()
        {
            _generalTab = new Tab(_generalTabContent, _generalButton);
            _audioTab = new Tab(_audioTabContent, _audioButton);
            _customizeTab = new Tab(_customizeTabContent, _customizeButton);
            
            _generalTab.AddListener(OnGeneralButtonClicked);
            _audioTab.AddListener(OnAudioButtonClicked);
            _customizeTab.AddListener(OnCustomizeButtonClicked);
        }

        private void OnEnable()
        {
            SwitchToTab(_generalTab);
        }

        private void OnDisable()
        {
            ResetAllTabs();
        }

        private void OnGeneralButtonClicked()
        {
            SwitchToTab(_generalTab);
        }


        private void OnAudioButtonClicked()
        {
            SwitchToTab(_audioTab);
        }

        private void OnCustomizeButtonClicked()
        {
            SwitchToTab(_customizeTab);
        }

        private async void SwitchToTab(Tab target)
        {
            if (CurrentTab.Content == target.Content)
                return;

            if (CurrentTab.Content != null)
            {
                List<Task> outTasks = new List<Task>();
                outTasks.Add(FadeSetTabActive(CurrentTab, false));
                outTasks.Add(_colorToOutAnim.PerformAsync(CurrentTab.ButtonText));
                await Task.WhenAll(outTasks);
            }

            CurrentTab = target;
            List<Task> inTasks = new List<Task>();
            inTasks.Add(FadeSetTabActive(CurrentTab, true));
            inTasks.Add(_colorToInAnim.PerformAsync(CurrentTab.ButtonText));
            await Task.WhenAll(inTasks);
        }

        private async Task FadeSetTabActive(Tab target, bool toActivate)
        {
            var curreCanvasGroup = target.Content;
            if (toActivate)
            {
                await _tabFadeInAnim.PerformAsync(curreCanvasGroup, () => { curreCanvasGroup.gameObject.SetActive(true); }, null, () =>
                {
                    curreCanvasGroup.interactable = true;
                    curreCanvasGroup.blocksRaycasts = true;
                });
            }
            else
            {
                await _tabFadeOutAnim.PerformAsync(curreCanvasGroup, () =>
                {
                    curreCanvasGroup.interactable = false;
                    curreCanvasGroup.blocksRaycasts = false;
                }, null, () => { curreCanvasGroup.gameObject.SetActive(false); });
            }
        }

        private void ResetAllTabs()
        {
            ResetTab(_generalTab.Content);
            ResetTab(_audioTab.Content);
            ResetTab(_customizeTab.Content);

            void ResetTab(CanvasGroup item)
            {
                item.gameObject.SetActive(false);
                item.alpha = 0f;
                item.interactable = false;
                item.blocksRaycasts = false;
            }
        }
    }

    [Serializable]
    public struct Tab
    {
        public CanvasGroup Content { get; }

        public ButtonController ButtonController { get; }

        public TextMeshProUGUI ButtonText { get; }

        public Tab(CanvasGroup content, ButtonController buttonController)
        {
            Content = content;
            ButtonController = buttonController;
            ButtonText = buttonController.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void AddListener(Action callback) => ButtonController.OnButtonClick += callback;
    }
}