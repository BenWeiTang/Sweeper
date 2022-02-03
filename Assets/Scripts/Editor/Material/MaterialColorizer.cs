using UnityEngine;
using UnityEditor;

namespace Minesweeper.Editor
{
    public class MaterialColorizer : EditorWindow
    {
        private static MaterialColorPalette _mcp;
        private static Material[] _materials = new Material[8];
        private static Color[] _colors = new Color[8];
        private Color[] _targetColors;


        [MenuItem("3D Minesweeper/Material Colorizer")]
        public static void OpenWindow()
        {
            GetWindow<MaterialColorizer>("Material Colorizer");
        }

        private void OnGUI()
        {
            DrawMCP();
        }

        private void DrawMCP()
        {
            GUILayout.Space(10f);
            _mcp = (MaterialColorPalette)EditorGUILayout.ObjectField("Material Color Palette", _mcp, typeof(MaterialColorPalette), false);

            if (_mcp != null)
            {
                GUILayout.Space(10f);
                GUILayout.Label("Current Colors", EditorStyles.boldLabel);

                foreach (var mc in _mcp.materialColors)
                {
                    EditorGUILayout.ColorField(mc.name, mc.material.color);
                }

                GUILayout.Space(10f);
                GUILayout.Label("Target Colors", EditorStyles.boldLabel);

                int count = _mcp.materialColors.Count;
                
                // First initialized as the same as the current color
                if (_targetColors == null || _targetColors.Length == 0)
                {
                    _targetColors = new Color[count];

                    for (int i = 0; i < count; i++)
                    {
                        _targetColors[i] = _mcp.materialColors[i].material.color;
                    }
                }

                for (int i = 0; i < count; i++)
                {
                    _targetColors[i] = (Color)EditorGUILayout.ColorField(_mcp.materialColors[i].name, _targetColors[i]);
                }
                
                DrawButtons();
            }
        }

        private void DrawButtons()
        {
            GUILayout.Space(10f);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset"))
            {
                for (int i = 0; i < _mcp.materialColors.Count; i++)
                {
                    _targetColors[i] = _mcp.materialColors[i].material.color;
                }
            }

            if (GUILayout.Button("Apply"))
            {
                for (int i = 0; i < _materials.Length; i++)
                {
                    _mcp.materialColors[i].material.color = _targetColors[i];
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}
