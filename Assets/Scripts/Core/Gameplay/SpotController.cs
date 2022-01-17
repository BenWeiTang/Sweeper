using System;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using TMPro;
using Minesweeper.Event;
using DG.Tweening;

namespace Minesweeper.Core
{
    public class SpotController : MonoBehaviour, ISpot
    {
        [SerializeField] internal SpotAnimationController animController;

        [Header("Event")]
        [SerializeField] private IntEvent SafeSpotDug;
        [SerializeField] private VoidEvent FirstSafeSpotDig;
        [SerializeField] private IntEvent SafeSpotDigAt;
        [SerializeField] private IntEvent SpotMarkAt;
        [SerializeField] private IntEvent MineDigAt;
        [SerializeField] private BoolEvent GameFinish;

        public Spot spot { get; set; }
        public int IndexInGrid { get; private set; } = -1;

        private GridController _gridController;

        public void SetGridController(GridController controller) => _gridController = controller;
        public void SetIndexInGrid(int index) => IndexInGrid = index;

        public void Dig()
        {
            //TODO: Allow for 1 stored click
            if (animController.AnimIsPlaying)
                return;

            if (spot.State == SpotState.Untouched)
            {
                // Always make the first click safe
                if (!_gridController.HasBegun)
                {
                    _gridController.HasBegun = true;
                    spot.SetMine(false);
                    var adjacentSC = _gridController.GetAdjacentSpotControllers(this);
                    foreach (var sc in adjacentSC)
                    {
                        sc.spot.SetMine(false);
                    }

                    FirstSafeSpotDig.Raise();
                }

                if (spot.IsMine)
                {
                    spot.State = SpotState.Detonated;
                    MineDigAt.Raise(IndexInGrid);
                    GameFinish.Raise(false);
                }
                else
                {
                    spot.State = SpotState.Dug;
                    SafeSpotDigAt.Raise(IndexInGrid); // See _animController.OnSafeSpotDugAt
                    SafeSpotDug.Raise(spot.HintNumber);
                    AutoNearClear();
                }
            }

            void AutoNearClear()
            {
                // Cannot auto clear near if HintNumber of current spot is not 0
                if (spot.HintNumber != 0) return;

                var adjacentSC = _gridController.GetAdjacentSpotControllers(this);
                foreach (SpotController ac in adjacentSC)
                {
                    ac.Dig();
                }
            }

        }

        public void Mark()
        {
            //TODO: Allow for 1 stored click
            if (animController.AnimIsPlaying)
                return;

            if (spot.State == SpotState.Untouched)
            {
                // Don't allow for marking in the beginning of the game
                if (!_gridController.HasBegun)
                    return;

                spot.State = SpotState.Marked;
                SpotMarkAt.Raise(IndexInGrid); // See _animController.OnSpotMarkAt
            }
            else if (spot.State == SpotState.Marked)
            {
                spot.State = SpotState.Untouched;
                SpotMarkAt.Raise(IndexInGrid); // See _animController.OnSpotMarkAt
            }
        }

        public void ClearNear()
        {
            // Cannot clear near if current spot is not dug or if current dug spot has value 0
            if (spot.State != SpotState.Dug || spot.HintNumber == 0) return;

            // Cannot clear near if not having enough marked spots
            var adjacentSC = _gridController.GetAdjacentSpotControllers(this);
            int adjacentMarkNum = adjacentSC.Count(aj => aj.spot.State == SpotState.Marked);
            if (adjacentMarkNum < spot.HintNumber) return;

            foreach (SpotController ac in adjacentSC)
            {
                ac.Dig();
            }
        }

        public async Task ResetSpot()
        {
            spot.SetMine(false);
            spot.State = SpotState.Untouched;
            await animController.ShakeToBlock(Block.Untouched, 0.75f, Vector3.one * 10f, 50, 30f);
        }

        private void Start()
        {
            transform.localScale = Vector3.one * GameManager.Instance.CurrentLayout.SpotSize;
        }
    }
}
