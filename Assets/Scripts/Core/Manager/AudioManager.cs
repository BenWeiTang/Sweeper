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
        // [SerializeField] private SoundBank _BGMSoundBank;
        // [SerializeField] private SoundBank _gameplayEffectSoundBank;
        // [SerializeField] private SoundBank _UIEffectSoundBank;

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

        }
        public void OnCombo(int combo)
        {
            if (combo > _bigShake.value)
            {

            }
            else if (combo > _smallShake.value)
            {

            }
            else if (combo == 0)
            {

            }
            else
            {

            }
        }
        public void OnMineDetonated()
        {

        }
        #endregion
        #region PRIVATE_METHODS
        private void PlaySound(AudioSource audioSource, Track track)
        {
            audioSource.clip = track.track;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            audioSource.clip = null;
        }
        #endregion
    }
}
