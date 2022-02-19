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

        private void Awake()
        {
            _audioSources = new Queue<AudioSource>();
            IncreasePoolSize(_defualtCount);
            _template.gameObject.SetActive(false);
        }

        /// <summary>
        /// Play an Audio Clip using an Audio Source from the Audio Source pool.
        /// After the Audio Clip finishes playing, the Audio Source will be returned to the Audio Source pool.
        /// </summary>
        /// <param name="clip">The Audio Clip to play.</param>
        public void PlayClip(AudioClip clip)
        {
            if (_audioSources.Count == 0)
            {
                // for (int i = 0; i < _defualtCount; i++)
                // {
                //     var instance = Instantiate(_template, Vector3.zero, Quaternion.identity);
                //     instance.transform.SetParent(transform);
                //     _audioSources.Enqueue(instance);
                // }
                IncreasePoolSize(2);
            }

            var audioSource = _audioSources.Dequeue();
            audioSource.gameObject.SetActive(true);
            audioSource.clip = clip;
            audioSource.Play();
            StartCoroutine(ReleaseAfterPlaying(audioSource));
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