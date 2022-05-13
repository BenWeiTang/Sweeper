using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper.Audio
{
    public class AudioPool : MonoBehaviour
    {
        [Tooltip("The number of clones of the template will be instantiated in the beginning of the game.")]
        [SerializeField] private int _defualtCount = 5;
        
        [SerializeField, Tooltip("Force a playing Audio Source to be released after seconds since playing. Set to a negative number to never force release.")] 
        private float _forceReleaseAfterSeconds = 5f;
        
        [Tooltip("The template Audio Source to be cloned. " +
                 "Note that this template will not be used during gameplay. " +
                 "Also note that the output should be set up properly as all clones will use the same output.")]
        [SerializeField] private AudioSource _template;

        private Queue<AudioSource> _audioSources;
        private List<AudioSource> _inUseAudioSources;

        private void Awake()
        {
            _audioSources = new Queue<AudioSource>();
            _inUseAudioSources = new List<AudioSource>();
            IncreasePoolSize(_defualtCount);
            _template.gameObject.SetActive(false);
        }

        /// <summary>
        /// Play an Audio Clip using an Audio Source from the Audio Source pool.
        /// After the Audio Clip finishes playing, the Audio Source will be returned to the Audio Source pool.
        /// </summary>
        /// <param name="clip">The Audio Clip to play.</param>
        /// <param name="autoRelease">Should the clip be automatically stopped and released after 5 seconds?</param>
        public void PlayClip(AudioClip clip, bool autoRelease)
        {
            if (_audioSources.Count == 0)
            {
                IncreasePoolSize(2);
            }

            var audioSource = _audioSources.Dequeue();
            _inUseAudioSources.Add(audioSource);
            audioSource.gameObject.SetActive(true);
            audioSource.clip = clip;
            audioSource.Play();
            
            if (autoRelease)
            {
                StartCoroutine(ReleaseAfterPlaying(audioSource));
            }
        }

        public void StopAll()
        {
            for (int i = _inUseAudioSources.Count - 1; i >= 0; i--)
            {
                var audioSource = _inUseAudioSources[i];
                audioSource.Stop();
                audioSource.clip = null;
                audioSource.gameObject.SetActive(false);
                _audioSources.Enqueue(audioSource);
                _inUseAudioSources.Remove(audioSource);
            }
        }

        private IEnumerator ReleaseAfterPlaying(AudioSource audioSource)
        {
            var startTime = Time.time;
            while (audioSource.timeSamples < audioSource.clip.samples)
            {
                if (_forceReleaseAfterSeconds > 0f && Time.time - startTime > _forceReleaseAfterSeconds)
                {
                    break;
                }
                
                yield return null;
            }
            
            audioSource.Stop();
            audioSource.clip = null;
            audioSource.gameObject.SetActive(false);
            _audioSources.Enqueue(audioSource);
            _inUseAudioSources.Remove(audioSource);
        }

        private void IncreasePoolSize(int delta)
        {
            for (int i = 0; i < delta; i++)
            {
                var instance = Instantiate(_template, Vector3.zero, Quaternion.identity);
                instance.transform.SetParent(transform);
                instance.clip = null;
                instance.gameObject.SetActive(false);
                _audioSources.Enqueue(instance);
            }
        }
    }
}