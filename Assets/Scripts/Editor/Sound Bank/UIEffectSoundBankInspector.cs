using UnityEngine;
using UnityEditor;
using Minesweeper.Audio;

namespace Minesweeper.Editor
{
    [CustomEditor(typeof(UIEffectSoundBank))]
    public class UIEffectSoundBankInspector : ASoundBankInspector<UIEffectSoundBank, UISoundEffect>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            UIEffectSoundBank instance = target as UIEffectSoundBank;
            DrawButton(instance);
        }
        protected override void DrawButton(UIEffectSoundBank instance)
        {
            if (GUILayout.Button("Update"))
            {

            }
        }
    }
}
