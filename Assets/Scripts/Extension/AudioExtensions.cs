using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Minesweeper.Extensions
{
    public static class AudioExtensions
    {
        private const float InverseE = 1 / (float) Math.E;
        
        /// <summary>
        /// Change the volume of this Audio Mixer.
        /// </summary>
        /// <param name="mixer"></param>
        /// <param name="exposedName">The name of 'The Exposed to Script' variable</param>
        /// <param name="value">value must be between 0 and 1</param>
        public static void SetVolume(this AudioMixer mixer, string exposedName, float value)
        {
            value = Mathf.Clamp01(value);
            value = Mathf.Pow(value, InverseE);
            mixer.SetFloat(exposedName, Mathf.Lerp(-80.0f, 0.0f, value));
        }
 
        /// <summary>
        /// Get the volume of this Audio Mixer.
        /// </summary>
        /// <param name="mixer"></param>
        /// <param name="exposedName">The name of 'The Exposed to Script' variable</param>
        /// <returns></returns>
        public static float GetVolume(this AudioMixer mixer, string exposedName)
        {
            if (mixer.GetFloat(exposedName, out float volume))
            {
                float result = Mathf.InverseLerp(-80.0f, 0.0f, volume);
                result = Mathf.Pow(result, (float) Math.E);
                return result;
            }
 
            return 0f;
        }
    }
}