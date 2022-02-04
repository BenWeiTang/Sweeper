using UnityEngine;

namespace Minesweeper.Audio
{
    [CreateAssetMenu(fileName = "New Track", menuName = "3D Minesweeper/Audio/Track", order = 99)]
    public class Track : ScriptableObject
    {
        public AudioClip track;
        public string trackName;
        public string author;
    }
}
