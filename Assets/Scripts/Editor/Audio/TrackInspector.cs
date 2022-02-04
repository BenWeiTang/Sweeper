using UnityEngine;
using UnityEditor;
using Minesweeper.Core;
using Minesweeper.Audio;

namespace Minesweeper.Editor
{
    [CustomEditor(typeof(Track))]
    public class TrackInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Track instance = target as Track;

            GUILayout.BeginHorizontal();
            DrawPlayButton(instance);
            DrawStopButton();
            GUILayout.EndHorizontal();
        }

        private void DrawPlayButton(Track instance)
        {
            if (GUILayout.Button("Play"))
            {
                AudioUtility.PlayClip(instance.track);
            }
        }
        private void DrawStopButton()
        {
            if (GUILayout.Button("Stop"))
            {
                AudioUtility.StopAllClips();
            }
        }
    }
}
