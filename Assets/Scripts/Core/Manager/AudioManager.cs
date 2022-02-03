using UnityEngine;
using UnityEngine.Audio;
using Minesweeper.Audio;

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

        [Header("Sound Banks")]
        [SerializeField] private SoundBank _BGMSoundBank;
        [SerializeField] private SoundBank _gameplayEffectSoundBank;
        [SerializeField] private SoundBank _UIEffectSoundBank;

        protected override void Awake()
        {
            base.Awake();
        }
    }
}
