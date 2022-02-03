using UnityEditor;
using UnityEngine;
using Minesweeper.Core;

namespace Minesweeper.Editor
{
    [CustomEditor(typeof(GridController))]
    public class GridControllerInspector : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GridController instance = target as GridController;

            GUILayout.Space(10f);
            DrawCheat(instance);
        }

        private void DrawCheat(GridController instance)
        {
            GUILayout.Label("Cheat", EditorStyles.boldLabel);
            if (!EditorApplication.isPlaying) return;
            if (GUILayout.Button("Show Everything"))
            {
                instance.HideEverything();
                instance.ShowEverything();
            }
            if (GUILayout.Button("Show Mines"))
            {
                instance.HideEverything();
                instance.ShowMines();
            }
            if (GUILayout.Button("Show Everything Except Mines"))
            {
                instance.HideEverything();
                instance.ShowEverythingExceptMines();
            }
            if (GUILayout.Button("Hide Everything"))
            {
                instance.HideEverything();
            }
        }
    }
}