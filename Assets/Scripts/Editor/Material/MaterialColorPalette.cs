using System.Collections.Generic;
using UnityEngine;

namespace Minesweeper.Editor
{
    [CreateAssetMenu(fileName = "New Material Color Palette", menuName = "3D Minesweeper/Material Color Palette")]
    public class MaterialColorPalette : ScriptableObject
    {
        public List<MaterialColor> materialColors = new List<MaterialColor>();
    }

    [System.Serializable]
    public struct MaterialColor
    {
        public string name;
        public Material material;
    }
}
