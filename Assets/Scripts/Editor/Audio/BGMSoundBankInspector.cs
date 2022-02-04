using UnityEngine;
using UnityEditor;
using Minesweeper.Audio;

namespace Minesweeper.Editor
{
    [CustomEditor(typeof(BGMSoundBank))]
    public class BGMSoundBankInspector : ASoundBankInspector<BGMSoundBank, BGMType>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            BGMSoundBank instance = target as BGMSoundBank;
            DrawButton(instance);
        }

        protected override void DrawButton(BGMSoundBank instance)
        {
            if (GUILayout.Button("Update"))
            {
                instance.UpdatePlayList();
            }
        }
    }
}