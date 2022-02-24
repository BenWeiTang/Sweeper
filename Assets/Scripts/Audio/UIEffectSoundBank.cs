using UnityEngine;

namespace Minesweeper.Audio
{
    [CreateAssetMenu(fileName = "UI Effect Sound Bank", menuName = "3D Minesweeper/Audio/UI Effect SoundBank")]
    public class UIEffectSoundBank : ASoundBank<UISoundEffect>
    {
    }

    public enum UISoundEffect
    {
        ButtonEnter,
        ButtonClicked,
        Confirm,
    }
}
