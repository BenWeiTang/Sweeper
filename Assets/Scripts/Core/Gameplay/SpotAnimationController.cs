using System;
using System.Threading.Tasks;
using UnityEngine;
using Minesweeper.Animation;
using DG.Tweening;

namespace Minesweeper.Core
{
    public class SpotAnimationController : MonoBehaviour
    {
        [SerializeField] private SpotController _spotController;

        [SerializeField] private GameObject[] _numBlocks;

        [Header("Special Blocks")]
        [SerializeField] private GameObject _untouchedBlock;
        [SerializeField] private GameObject _markedBlock;
        [SerializeField] private GameObject _mineBlock;

        [Header("Animation Plug-ins")]
        [SerializeField] private Bounce _bounceAnim;
        [SerializeField] private ShakeToBlock _shakeToBlockAnim;

        internal bool AnimIsPlaying = false;

        private GameObject _currentBlock;

        public async void OnSafeSpotDugAt(int index)
        {
            if (index == _spotController.IndexInGrid)
            {
                await Bounce(() => SwtichToBlock((Block)_spotController.spot.HintNumber));
            }
        }

        public async void OnSpotMarkedAt(int index)
        {
            if (index == _spotController.IndexInGrid)
            {
                if (_spotController.spot.State == SpotState.Untouched)
                {
                    await Bounce(() => SwtichToBlock(Block.Untouched));
                }
                else if (_spotController.spot.State == SpotState.Marked)
                {
                    await Bounce(() => SwtichToBlock(Block.Marked));
                }
            }
        }

        public async void OnMineDugAt(int index)
        {
            if (index == _spotController.IndexInGrid)
            {
                await Bounce(() => SwtichToBlock(Block.Mine));
            }
        }

        internal void SwtichToBlock(Block toBlock)
        {
            _currentBlock?.SetActive(false);
            switch (toBlock)
            {
                case Block.Untouched:
                    _currentBlock = _untouchedBlock;
                    break;
                case Block.Marked:
                    _currentBlock = _markedBlock;
                    break;
                case Block.Mine:
                    _currentBlock = _mineBlock;
                    break;
                default:
                    _currentBlock = _numBlocks[(int)toBlock];
                    break;
            }
            _currentBlock.SetActive(true);
        }

        internal async Task ShakeToBlock(Block toBlock, float duration, Vector3 strength, int vibrato, float randomness, bool fadeOut = false)
        {
            await _shakeToBlockAnim.PerformAsync(transform, null, () => SwtichToBlock(toBlock), null);
        }

        internal async Task Bounce(Action atPeak)
        {
            AnimIsPlaying = true;
            await _bounceAnim.PerformAsync(transform, null, atPeak, null);
            AnimIsPlaying = false;
        }

        private void Start()
        {
            SwtichToBlock(Block.Untouched);
        }
    }

    // Do NOT move the order from Zero to One, casting to int in use
    public enum Block
    {
        Zero,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Untouched,
        Marked,
        Mine,
    }
}
