using System;
using System.Threading.Tasks;
using UnityEngine;
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

        internal bool AnimIsPlaying = false;

        private GameObject _currentBlock;

        public async void OnSafeSpotDugAt(int index)
        {
            if (index == _spotController.IndexInGrid)
            { 
                Action atPeak = () => SwtichToBlock((Block)_spotController.spot.HintNumber);
                await Bounce(0.1f, 0.2f, -0.15f, Ease.Linear, Ease.OutBounce, atPeak);
            }
        }

        public async void OnSpotMarkedAt(int index)
        {
            if (index == _spotController.IndexInGrid)
            {
                Action atPeak = null;
                if (_spotController.spot.State == SpotState.Untouched)
                    atPeak += () => SwtichToBlock(Block.Untouched);
                else if (_spotController.spot.State == SpotState.Marked)
                    atPeak += () => SwtichToBlock(Block.Marked);
                
                await Bounce(0.1f, 0.2f, -0.22f, Ease.Linear, Ease.OutBounce, atPeak);
            }
        }

        public async void OnMineDugAt(int index)
        {
            if (index == _spotController.IndexInGrid)
            {
                Action atPeak = ()=>SwtichToBlock(Block.Mine);
                await Bounce(0.1f, 0.2f, -0.15f, Ease.Linear, Ease.OutBounce, atPeak);
            }
        }

        internal void SwtichToBlock(Block toBlock)
        {
            _currentBlock?.SetActive(false);
            switch(toBlock)
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

        internal async Task Bounce(float inDuration, float outDuration, float delta, Ease inEase, Ease outEase, Action atPeak)
        {
            AnimIsPlaying = true;
            Sequence s = DOTween.Sequence();
            float ogScaleFactor = transform.localScale.x;
            float endValue = ogScaleFactor + delta;
            s.Append(transform.DOScale(endValue, inDuration).SetEase(inEase));
            atPeak?.Invoke();
            s.Append(transform.DOScale(ogScaleFactor, outDuration).SetEase(outEase));
            var currentTask = s.AsyncWaitForCompletion();

            await currentTask;
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
