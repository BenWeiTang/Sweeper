using UnityEngine;
using UnityEngine.Audio;

namespace Minesweeper.Core
{
    public class AudioManager : AManager<AudioManager>
    {
        [Header("Audio Mixer")]
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private AudioMixerGroup _master;
        [SerializeField] private AudioMixerGroup _BGM;
        [SerializeField] private AudioMixerGroup _effect;

        [Header("Audio Source")]
        [SerializeField] private AudioSource _BGMSource;
        [SerializeField] private AudioSource _effectSource;

        // [Header("Sound Banks")]
        // [SerializeField] private 
        protected override void Awake()
        {
            base.Awake();
        }
    }
}
