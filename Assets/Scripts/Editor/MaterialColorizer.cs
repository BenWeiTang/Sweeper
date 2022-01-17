using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Minesweeper.Editor
{
    public class MaterialColorizer : EditorWindow
    {
        private static Material[] _materials = new Material[8];
        private static Color[] _colors = new Color[8];


        [MenuItem("3D Minesweeper/Material Colorizer")]
        public static void OpenWindow()
        {
            GetWindow<MaterialColorizer>("Material Colorizer");
        }

        private void OnGUI()
        {
            DrawMaterialFields();
            DrawColorFields();
            DrawButton();
        }

        private void DrawMaterialFields()
        {
            GUILayout.Space(10f);
            GUILayout.Label("Target Materials", EditorStyles.boldLabel);

            _materials[0] = (Material)EditorGUILayout.ObjectField("Dug Base", _materials[0], typeof(Material), false);
            _materials[1] = (Material)EditorGUILayout.ObjectField("Dug Edge", _materials[1], typeof(Material), false);

            GUILayout.Space(5f);

            _materials[2] = (Material)EditorGUILayout.ObjectField("Marked Base", _materials[2], typeof(Material), false);
            _materials[3] = (Material)EditorGUILayout.ObjectField("Marked Edge", _materials[3], typeof(Material), false);

            GUILayout.Space(5f);

            _materials[4] = (Material)EditorGUILayout.ObjectField("Mine Base", _materials[4], typeof(Material), false);
            _materials[5] = (Material)EditorGUILayout.ObjectField("Mine Edge", _materials[5], typeof(Material), false);

            GUILayout.Space(5f);

            _materials[6] = (Material)EditorGUILayout.ObjectField("Untouched Base", _materials[6], typeof(Material), false);
            _materials[7] = (Material)EditorGUILayout.ObjectField("Untouchec Edge", _materials[7], typeof(Material), false);
        }

        private void DrawColorFields()
        {
            GUILayout.Space(10f);
            GUILayout.Label("Target Colors", EditorStyles.boldLabel);

            _colors[0] = EditorGUILayout.ColorField("Dug Base", _colors[0]);
            _colors[1] = EditorGUILayout.ColorField("Dug Edge", _colors[1]);

            GUILayout.Space(5f);

            _colors[2] = EditorGUILayout.ColorField("Marked Base", _colors[2]);
            _colors[3] = EditorGUILayout.ColorField("Marked Edge", _colors[3]);

            GUILayout.Space(5f);

            _colors[4] = EditorGUILayout.ColorField("Mine Base", _colors[4]);
            _colors[5] = EditorGUILayout.ColorField("Mine Edge", _colors[5]);

            GUILayout.Space(5f);

            _colors[6] = EditorGUILayout.ColorField("Untouched Base", _colors[6]);
            _colors[7] = EditorGUILayout.ColorField("Untouched Edge", _colors[7]);
        }

        private void DrawButton()
        {
            GUILayout.Space(10f);

            if (GUILayout.Button("Apply Colors"))
            {
                for (int i = 0; i < _materials.Length; i++)
                {
                    if (_materials[i] == null)
                        continue;
                    
                    _materials[i].color = _colors[i];
                }
            }
        }
    }
}
