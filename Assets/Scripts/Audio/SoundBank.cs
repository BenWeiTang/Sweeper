using UnityEngine;

namespace Minesweeper.Audio
{
    [CreateAssetMenu(fileName = "New Sound Bank", menuName = "3D Minesweeper/Audio/Sound Bank")]
    public class SoundBank : ScriptableObject
    {
        [SerializeField] private Track[] _tracks;
        public Track[] Tracks => _tracks;
        public int Count => _tracks.Length;
    }
}