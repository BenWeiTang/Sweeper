using UnityEngine;

namespace Minesweeper
{
    
    [CreateAssetMenu(fileName = "ColorConfiguror", menuName = "3D Minesweeper/Color Configuror", order = 0)]
    public class ColorConfiguror : ScriptableObject
    {
        public Material untouchedMaterial;
        public Material dugMaterial;
        public Material markedMaterial;
        public Material mineMaterial;
        public Material highlightedMaterial;

        public Color untouchedColor;
        public Color dugColor;
        public Color markedColor;
        public Color mineColor;
        public Color highlightedColor;

        public void UpdateColors()
        {
            UpdateMaterialColor(untouchedMaterial, untouchedColor);
            UpdateMaterialColor(dugMaterial, dugColor);
            UpdateMaterialColor(markedMaterial, markedColor);
            UpdateMaterialColor(mineMaterial, mineColor);
            UpdateMaterialColor(highlightedMaterial, highlightedColor);
        }

        private void UpdateMaterialColor(Material m, Color c)
        {
            m.SetColor("_BaseMap", c);
        }
    }
}
