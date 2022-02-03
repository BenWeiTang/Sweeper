using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper.Audio
{
    [CreateAssetMenu(fileName = "BGM Sound Bank", menuName = "3D Minesweeper/Audio/BGM SoundBank")]
    public class BGMSoundBank : ASoundBank<BGMType>
    {
    }

    public enum BGMType
    {
        StartScene,
        Gameplay
    }
}
