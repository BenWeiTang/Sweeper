using UnityEngine;

namespace Minesweeper.Core
{
    [CreateAssetMenu(fileName = "New Layout Setting", menuName = "3D Minesweeper/Layout Setting")]
    public class Layout : ScriptableObject
    {
        [Header("Grid Configuration")]
        [Min(5)] public int Width;

        [Min(5)] public int Height;

        [Min(5)] public int MineCount = 5;

        [Range(0f, 2f)] public float SpotSize = 0.4f;

        [Range(0f, 1f)] public float Spacing = 0.1f;

        [Header("Camera")]
        public Vector3 CameraDisplacement;

        [Header("Prefab")]
        public SpotController SpotPrefab;

        [Header("Tweening")]
        public float SpawnSphereRadius = 15f;

        private void OnValidate()
        {
            MineCount = Mathf.Min(MineCount, Mathf.FloorToInt(Height * Width * 0.2063f));
        }
    }
}