using UnityEngine;
using UnityEditor;
using Minesweeper.Audio;

namespace Minesweeper.Editor
{
    public abstract class ASoundBankInspector<T, E> : UnityEditor.Editor 
        where T : ASoundBank<E>
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(10f);
        }

        protected abstract void DrawButton(T instance);
    }
}
