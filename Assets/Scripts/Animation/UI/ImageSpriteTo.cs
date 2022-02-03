using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "Image Sprite To", menuName = "3D Minesweeper/Animation/UI/Image Sprite To")]
    public class ImageSpriteTo : ASerializedTargetAnimation<Image>
    {
        [SerializeField] private Sprite _targetSprite;

        public override async Task PerformAsync(Image item, Action onEnter = null, Action onPeak = null, Action onExit = null)
        {
            await Task.Yield();
            item.sprite = _targetSprite;
        }
    }
}
