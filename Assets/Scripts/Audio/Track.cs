using UnityEngine;

namespace Minesweeper.Audio
{
    [CreateAssetMenu(fileName = "New Track", menuName = "3D Minesweeper/Audio/Track")]
    public class Track : ScriptableObject
    {
        public AudioClip track;
        public string trackName;
        public int id = -1;
        public string author;
    }
}
