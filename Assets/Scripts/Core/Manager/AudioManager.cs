using UnityEngine;
using UnityEngine.SceneManagement;
using Minesweeper.Audio;
using Minesweeper.Reference;
using Minesweeper.Scene;

namespace Minesweeper.Core
{
    public class AudioManager : AManager<AudioManager>
    {
        // [Header("Audio Mixer")]
        // [SerializeField] private AudioMixer _mixer;
        // [SerializeField] private AudioMixerGroup _masterGroup;
        // [SerializeField] private AudioMixerGroup _BGMGroup;
        // [SerializeField] private AudioMixerGroup _effectGroup;

        [Header("Audio Source")]
        [SerializeField] private AudioPool _BGMPool;
        [SerializeField] private AudioPool _SFXPool;

        [Header("Sound Bank")]
        private BGMSoundBank _BGMSoundBank;
        private GameplayEffectSoundBank _gameplayEffectSoundBank;
        [SerializeField] private UIEffectSoundBank _UIEffectSoundBank;

        [Header("Camera Shake Threshold References")]
        [SerializeField] private IntRef _bigShake;
        [SerializeField] private IntRef _smallShake;

        #region PUBLIC_CALLBACKS

        public void OnMarked(int _) => PlayGameplayEffect(GameplaySoundEffect.Mark);

        public void OnCombo(int combo)
        {
            if (combo > _bigShake.value)
                PlayGameplayEffect(GameplaySoundEffect.LargeClear);
            else if (combo > _smallShake.value)
                PlayGameplayEffect(GameplaySoundEffect.MediumClear);
            else if (combo == 0)
                PlayGameplayEffect(GameplaySoundEffect.ZeroClear);
            else
                PlayGameplayEffect(GameplaySoundEffect.SmallClear);
        }

        public void OnGridBounceAllStarted() => PlayGameplayEffect(GameplaySoundEffect.GridBounceAll);

        public void OnGridFloatAllStarted() => PlayGameplayEffect(GameplaySoundEffect.GridFloatAll);

        public void OnGridMoveInStarted() => PlayGameplayEffect(GameplaySoundEffect.GridMoveIn);

        public void OnGridRotateAllStarted() => PlayGameplayEffect(GameplaySoundEffect.GridRotateAll);

        public void OnSpotShakeToBlockStarted() => PlayGameplayEffect(GameplaySoundEffect.SpotShakeToBlock);

        public void OnMineDetonated() => PlayGameplayEffect(GameplaySoundEffect.Explosion);

        public void OnPointerEnteredUIElement() => PlayUIEffect(UISoundEffect.ButtonEnter);

        public void OnPointerClickedUIElement(bool isConfirmation) => PlayUIEffect(isConfirmation ? UISoundEffect.Confirm : UISoundEffect.ButtonClicked);

        public void OnGameReady() => PlayBGM(BGMType.Gameplay);

        public void OnGameFinished(bool _) => StopBGM();

        #endregion

        #region UNITY_METHODS

        protected override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += ((scene, _) =>
            {
                // When entering the Gameplay or StartMenu scene, reload the two sound banks
                // The reason why checking against StartMenu scene is needed, is that the 
                // deco spots in that scene has a callback that in turn uses the GameplayEffectSoundBank
                if (LevelSystem.IsSameScene(SceneIndex.Gameplay, scene) || LevelSystem.IsSameScene(SceneIndex.StartMenu, scene))
                {
                    var currentSettings = SettingsManager.Instance.CurrentTheme;
                    _BGMSoundBank = currentSettings.BGMSoundBank;
                    _gameplayEffectSoundBank = currentSettings.GameplayEffectSoundBank;
                }
            });
        }

        #endregion
        
        #region PRIVATE_METHODS
        private void PlayGameplayEffect(GameplaySoundEffect effect) => PlaySound(_SFXPool, _gameplayEffectSoundBank.GetTrack(effect));

        private void PlayBGM(BGMType type) => PlaySound(_BGMPool, _BGMSoundBank.GetTrack(type), false);

        private void StopBGM() => _BGMPool.StopAll();

        private void PlayUIEffect(UISoundEffect effect) => PlaySound(_SFXPool, _UIEffectSoundBank.GetTrack(effect));

        private static void PlaySound(AudioPool pool, Track track, bool autoRelease = true) => pool.PlayClip(track.track, autoRelease);

        #endregion
    }
}
