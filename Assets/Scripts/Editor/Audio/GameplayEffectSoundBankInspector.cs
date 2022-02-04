using UnityEngine;
using UnityEditor;
using Minesweeper.Audio;

namespace Minesweeper.Editor
{
    [CustomEditor(typeof(GameplayEffectSoundBank))]
    public class GameplayEffectSoundBankInspector : ASoundBankInspector<GameplayEffectSoundBank, GameplaySoundEffect>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GameplayEffectSoundBank instance = target as GameplayEffectSoundBank;
            DrawButton(instance);
        }
        protected override void DrawButton(GameplayEffectSoundBank instance)
        {
            if (GUILayout.Button("Update"))
            {
                instance.UpdatePlaylist();
            }
        }
    }
}
