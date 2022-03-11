using UnityEngine;

namespace Minesweeper.Core
{
    [CreateAssetMenu(fileName = "Theme", menuName = "3D Minesweeper/Theme")]
    public class Theme : ScriptableObject
    {
        [Header("Body")]
        public Color DugBase;
        public Color DugEdge;
        public Color MarkedBase;
        public Color MarkedEdge;
        public Color MineBase;
        public Color MineEdge;
        public Color UntouchedBase;
        public Color UntouchedEdge;

        [Header("Number")]
        public Color One;
        public Color Two;
        public Color Three;
        public Color Four;
        public Color Five;
        public Color Six;
        public Color Seven;
        public Color Eight;

        [Header("Camera")]
        public Color BackgroundColor;
    }
}
