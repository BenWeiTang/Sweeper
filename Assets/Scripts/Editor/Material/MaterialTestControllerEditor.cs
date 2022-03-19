using UnityEngine;
using UnityEditor;

namespace Minesweeper.Editor
{
    [CustomEditor(typeof(MaterialTestController))]
    public class MaterialTestControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            MaterialTestController instance = target as MaterialTestController;
            GUILayout.Space(10f);
            DrawButton(instance);
        }

        private void DrawButton(MaterialTestController instance)
        {
            if (GUILayout.Button("Update"))
            {
                instance.UpdateMaterial();
            }
        }
    }
}
