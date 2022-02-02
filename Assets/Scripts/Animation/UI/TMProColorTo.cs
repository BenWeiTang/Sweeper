using System;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Minesweeper.Animation
{
    [CreateAssetMenu(fileName = "TMPro Color To", menuName = "3D Minesweeper/Animation/UI/TMPro Color To")]
    public class TMProColorTo : ATMProAnimation
    {
        [SerializeField] private Color _targetColor;
        [SerializeField] private float _duration;

        public override async Task PerformAsync(TextMeshProUGUI item, Action onEnter = null, Action _ = null, Action onExit = null)
        {
            onEnter?.Invoke();
            await item.DOColor(_targetColor, _duration).AsyncWaitForCompletion();
            onExit?.Invoke();
        }
    }
}
