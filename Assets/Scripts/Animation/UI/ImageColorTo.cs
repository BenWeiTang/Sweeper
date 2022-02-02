using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Image Color To", menuName = "3D Minesweeper/Animation/UI/Image Color To")]
    public class ImageColorTo : AImageAnimation
    {
        [SerializeField] private Color _targetColor;
        [SerializeField] private float _duration;

        public override async Task PerformAsync(Image image, Action onEnter = null, Action _ = null, Action onExit = null)
        {
            onEnter?.Invoke();
            await image.DOColor(_targetColor, _duration).AsyncWaitForCompletion();
            onExit?.Invoke();
        }
    }
}
