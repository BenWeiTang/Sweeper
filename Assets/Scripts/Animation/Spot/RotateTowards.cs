using System;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
using PlasticGui.WorkspaceWindow.BranchExplorer;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Rotate Towards", menuName = "3D Minesweeper/Animation/Grid/Rotate Towards")]
    public class RotateTowards : ADynamicTargetAnimation<Transform, Vector3>
    {
        [SerializeField, Range(0f, 2f)] private float _rotateTime;
        [SerializeField] private Ease _easeMode;
        [SerializeField] private RotateMode _rotateMode;

        public override Task PerformAsync(Transform item, Vector3 target, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            var task = item.DORotate(target, _rotateTime, _rotateMode).SetEase(_easeMode).AsyncWaitForCompletion();
            return task;
        }
    }
}