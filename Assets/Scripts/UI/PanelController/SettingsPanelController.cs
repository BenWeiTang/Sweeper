using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Minesweeper.Animation;
using Minesweeper.Saving;
using TMPro;

namespace Minesweeper.UI
{
    public class SettingsPanelController : APanelController
    {
        public override PanelType Type => PanelType.Settings;
        public Tab CurrentTab { get; private set; }

        [Header("General Tab")] 
        [SerializeField] private CanvasGroup _generalTabContent;
        [SerializeField] private ButtonController _generalButton;
        [SerializeField] private ToggleGroupController _difficulty;
        [SerializeField] private Toggle _useEasyClear;
        
        [Header("Audio Tab")]
        [SerializeField] private CanvasGroup _audioTabContent;
        [SerializeField] private ButtonController _audioButton;
        [SerializeField] private Toggle _muteMaster;
        [SerializeField] private Slider _masterVolume;
        [SerializeField] private Toggle _muteBgm;
        [SerializeField] private Slider _bgmVolume;
        [SerializeField] private Toggle _muteSfx;
        [SerializeField] private Slider _sfxVolume;
        
        [Header("Customize Tab")]
        [SerializeField] private CanvasGroup _customizeTabContent;
        [SerializeField] private ButtonController _customizeButton;

        [Header("Animation")] 
        [SerializeField] private CanvasGroupFade _tabFadeInAnim;
        [SerializeField] private CanvasGroupFade _tabFadeOutAnim;
        [SerializeField] private TMProColorTo _colorToInAnim;
        [SerializeField] private TMProColorTo _colorToOutAnim;

        private Tab _generalTab;
        private Tab _audioTab;
        private Tab _customizeTab;
        private readonly Tab _voidTab = new Tab();

        
        public void SaveSettings()
        {
            int newDifficulty = _difficulty.CurrentID switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                _ => 0
            };
            
            var newSettings = new MasterSettingsData
            {
                GeneralSettingsData =
                {
                    Difficulty = newDifficulty,
                    EasyClear = _useEasyClear.isOn
                },
                AudioSettingsData =
                {
                    MasterVolume = _masterVolume.value,
                    BGMVolume = _bgmVolume.value,
                    EffectVolume = _sfxVolume.value,
                    Mute = _muteMaster.isOn,
                    MuteEffect = _muteSfx.isOn,
                    MuteBGM = _muteBgm.isOn
                }
            };
            
            SettingsSerializer.SaveSettings(newSettings);
        }
        
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
            ReadFromJson();
        }

        private void OnDisable() => ResetAllTabs();

        private void OnGeneralButtonClicked() => SwitchToTab(_generalTab);

        private void OnAudioButtonClicked() => SwitchToTab(_audioTab);

        private void OnCustomizeButtonClicked() => SwitchToTab(_customizeTab);

        private void ReadFromJson()
        {
            var settings = SettingsSerializer.LoadSettings();
            int targetControllerID = settings.GeneralSettingsData.Difficulty switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                _ => 0
            };
            _difficulty.UpdateCurrentController(targetControllerID);
            _useEasyClear.isOn = settings.GeneralSettingsData.EasyClear;
            _masterVolume.value = settings.AudioSettingsData.MasterVolume;
            _bgmVolume.value = settings.AudioSettingsData.BGMVolume;
            _sfxVolume.value = settings.AudioSettingsData.EffectVolume;
            _muteMaster.isOn = settings.AudioSettingsData.Mute;
            _muteBgm.isOn = settings.AudioSettingsData.MuteBGM;
            _muteSfx.isOn = settings.AudioSettingsData.MuteEffect;
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
            var currentCanvasGroup = target.Content;
            if (toActivate)
            {
                await _tabFadeInAnim.PerformAsync(currentCanvasGroup,
                    () => { currentCanvasGroup.gameObject.SetActive(true); }, null, () =>
                    {
                        currentCanvasGroup.interactable = true;
                        currentCanvasGroup.blocksRaycasts = true;
                    });
            }
            else
            {
                await _tabFadeOutAnim.PerformAsync(currentCanvasGroup, () =>
                {
                    currentCanvasGroup.interactable = false;
                    currentCanvasGroup.blocksRaycasts = false;
                }, null, () => { currentCanvasGroup.gameObject.SetActive(false); });
            }
        }

        private void ResetAllTabs()
        {
            ResetTab(_generalTab.Content);
            ResetTab(_audioTab.Content);
            ResetTab(_customizeTab.Content);
            CurrentTab = _voidTab;

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