using UnityEngine;
using UnityEngine.Audio;
using Minesweeper.Audio;
using Minesweeper.Reference;

namespace Minesweeper.Core
{
    public class AudioManager : AManager<AudioManager>
    {
        [Header("Audio Mixer")]
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioMixerGroup _masterGroup;
        [SerializeField] private AudioMixerGroup _BGMGroup;
        [SerializeField] private AudioMixerGroup _effectGroup;

        [Header("Audio Source")]
        [SerializeField] private AudioSource _BGMSource;
        [SerializeField] private AudioSource _effectSource;

        [Header("Sound Banks & Tracks")]
        [SerializeField] private BGMSoundBank _BGMSoundBank;
        [SerializeField] private GameplayEffectSoundBank _gameplayEffectSoundBank;
        [SerializeField] private UIEffectSoundBank _UIEffectSoundBank;

        [Header("Camera Shake Threshold References")]
        [SerializeField] private IntRef _bigShake;
        [SerializeField] private IntRef _smallShake;

        protected override void Awake()
        {
            base.Awake();
        }

        #region PUBLIC_CALLBACKS
        public void OnMarked(int _)
        {
            // PlaySound(_effectSource, _gameplayEffectSoundBank.GetTrack(GameplaySoundEffect.Mark));
            PlayGameplayEffect(GameplaySoundEffect.Mark);
        }
        public void OnCombo(int combo)
        {
            if (combo > _bigShake.value)
            {
                PlayGameplayEffect(GameplaySoundEffect.LargeClear);
            }
            else if (combo > _smallShake.value)
            {
                PlayGameplayEffect(GameplaySoundEffect.MediumClear);
            }
            else if (combo == 0)
            {
                PlayGameplayEffect(GameplaySoundEffect.ZeroClear);
            }
            else
            {
                PlayGameplayEffect(GameplaySoundEffect.SmallClear);
            }
        }
        public void OnMineDetonated()
        {
            PlayGameplayEffect(GameplaySoundEffect.Explosion);
        }
        #endregion
        #region PRIVATE_METHODS
        private void PlayGameplayEffect(GameplaySoundEffect effect)
        {
            PlaySound(_effectSource, _gameplayEffectSoundBank.GetTrack(effect));
        }

        private void PlayBGM(BGMType type)
        {
            PlaySound(_BGMSource, _BGMSoundBank.GetTrack(type));
        }

        private void PlayUIEffect(UISoundEffect effect)
        {
            PlaySound(_effectSource, _UIEffectSoundBank.GetTrack(effect));
        }

        private void PlaySound(AudioSource audioSource, Track track)
        {
            if (audioSource.isPlaying) return;
            audioSource.clip = track.track;
            audioSource.Play();
        }
        #endregion
    }
}
