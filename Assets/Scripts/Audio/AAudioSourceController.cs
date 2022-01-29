using UnityEngine;

namespace Minesweeper.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public abstract class AAudioSourceController : MonoBehaviour
    {
        protected AudioSource _audioSource;

        protected void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }
    }
}
