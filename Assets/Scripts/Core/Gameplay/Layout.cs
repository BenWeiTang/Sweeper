using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Core
{
    [CreateAssetMenu(fileName = "New Layout Setting", menuName = "3D Minesweeper/Layout Setting")]
    public class Layout : ScriptableObject
    {
        [Header("Grid Configuration")]
        [Min(5)] public int Width;

        [Min(5)] public int Height;

        [Range(0f, 90f)] public float MinePercentage = 20f;

        [Range(0f, 2f)] public float SpotSize = 0.4f;

        [Range(0f, 1f)] public float Spacing = 0.1f;

        [Header("Camera")]
        public Vector3 CameraDisplacement;

        [Header("Prefab")]
        public SpotController SpotPrefab;

        [Header("Tweening")]
        public float SpawnSphereRadius = 15f;
        public float MinMoveTime = 4f;
        public float MaxMoveTime = 5f;
        public Ease EaseMode = Ease.OutQuad;
        [Range(0.1f, 2f)] public float BounceInDuration = 0.1f;
        [Range(0.1f, 2f)] public float BounceOutDuration = 0.1f;
        [Range(-0.5f, 0.5f)] public float BounceDelta = -0.1f;
        public Ease BounceEaseIn = Ease.Linear;
        public Ease BounceEasOut = Ease.OutBounce;

        [Header("Color Configuration")]
        public Color One = Color.white;
        public Color Two = Color.white;
        public Color Three = Color.white;
        public Color Four = Color.white;
        public Color Five = Color.white;
        public Color Six = Color.white;
        public Color Seven = Color.white;
        public Color Eight = Color.white;
    }
}