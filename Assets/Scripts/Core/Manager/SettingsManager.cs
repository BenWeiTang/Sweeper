using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using Minesweeper.Scene;
using Minesweeper.Saving;
using Minesweeper.UI;
using Minesweeper.Extensions;

namespace Minesweeper.Core
{
    public class SettingsManager : AManager<SettingsManager>
    {
        public MasterSettingsData MasterSettingsData => SettingsSerializer.LoadSettings();

        
        [Header("General")]
        [SerializeField] private ToggleGroupController _difficulty;
        [SerializeField] private Toggle _useEasyClear;
        
        [Header("Audio")]
        [SerializeField] private Toggle _muteMaster;
        [SerializeField] private Slider _masterVolume;
        [SerializeField] private Toggle _muteBgm;
        [SerializeField] private Slider _bgmVolume;
        [SerializeField] private Toggle _muteSfx;
        [SerializeField] private Slider _sfxVolume;
        [SerializeField] private AudioMixer _masterMixer;

        private const string MASTER_NAME = "Master Volume";
        private const string BGM_NAME = "BGM Volume";
        private const string SFX_NAME = "SFX Volume";

        private bool _firstLoaded = true;
        private int _toggleGroupControllerID;

        protected override void Awake()
        {
            base.Awake();

            SceneManager.sceneLoaded += (scene, _) =>
            {
                if (LevelSystem.IsSameScene(SceneIndex.StartMenu, scene) && _firstLoaded)
                {
                    StartCoroutine(LateAwake(() =>
                    {
                        _firstLoaded = false;
                        LoadSettings();
                        InitAudioMixer();
                    }));
                }
            };

            IEnumerator LateAwake(Action callback)
            {
                yield return null;
                callback?.Invoke();
            }
        }
        
        public void OnMasterVolumeChanged(float volume) => _masterMixer.SetVolume(MASTER_NAME, volume);
        public void OnBGMVolumeChanged(float volume) => _masterMixer.SetVolume(BGM_NAME, volume);
        public void OnSFXVolumeChanged(float volume) => _masterMixer.SetVolume(SFX_NAME, volume);
        public void OnMasterMuted(bool isMuted) => _masterMixer.SetVolume(MASTER_NAME, isMuted ? 0f : _masterVolume.value);
        public void OnBGMMuted(bool isMuted) => _masterMixer.SetVolume(BGM_NAME, isMuted ? 0f : _bgmVolume.value);
        public void OnSFXMuted(bool isMuted) => _masterMixer.SetVolume(SFX_NAME, isMuted ? 0f : _sfxVolume.value);

        public void RefreshUI()
        {
            LoadSettings();
        }

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

        private void LoadSettings()
        {
            var settings = SettingsSerializer.LoadSettings();
            _toggleGroupControllerID = settings.GeneralSettingsData.Difficulty switch
            {
                0 => 0,
                1 => 1,
                2 => 2,
                _ => 0
            };
            _difficulty.UpdateCurrentController(_toggleGroupControllerID);
            _useEasyClear.isOn = settings.GeneralSettingsData.EasyClear;
            _masterVolume.value = settings.AudioSettingsData.MasterVolume;
            _bgmVolume.value = settings.AudioSettingsData.BGMVolume;
            _sfxVolume.value = settings.AudioSettingsData.EffectVolume;
            _muteMaster.isOn = settings.AudioSettingsData.Mute;
            _muteBgm.isOn = settings.AudioSettingsData.MuteBGM;
            _muteSfx.isOn = settings.AudioSettingsData.MuteEffect;
        }

        private void InitAudioMixer()
        {
            var masterMuteFactor = _muteMaster.isOn ? 0f : 1f;
            var bgmMuteFactor = _muteBgm.isOn ? 0f : 1f;
            var sfxMuteFactor = _muteSfx.isOn ? 0f : 1f;
            _masterMixer.SetVolume(MASTER_NAME, _masterVolume.value * masterMuteFactor);
            _masterMixer.SetVolume(BGM_NAME, _bgmVolume.value * bgmMuteFactor);
            _masterMixer.SetVolume(SFX_NAME, _sfxVolume.value * sfxMuteFactor);
        }
    }
}