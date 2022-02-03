using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper.Audio
{
    [CreateAssetMenu(fileName = "Gameplay Effect Sound Bank", menuName = "3D Minesweeper/Audio/Gameplay Effect SoundBank")]
    public class GameplayEffectSoundBank : ASoundBank<GameplaySoundEffect>
    {
    }

    public enum GameplaySoundEffect
    {
        ZeroClear,
        SmallClear,
        MediumClear,
        LargeClear,
        Mark,
        Explosion
    }
}
