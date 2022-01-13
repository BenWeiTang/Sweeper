using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using TMPro;
using Minesweeper.Event;

namespace Minesweeper.Core
{
    public class SpotController : MonoBehaviour, ISpot
    {
        [Header("Event")]
        [SerializeField] private IntEvent SafeSpotDug;
        [SerializeField] private VoidEvent FirstSafeSpotDig;
        [SerializeField] private BoolEvent GameFinish;

        [Header("Appearances")]
        [SerializeField] private GameObject _untouchedBlock;
        [SerializeField] private GameObject _dugBlock;
        [SerializeField] private GameObject _markedBlock;
        [SerializeField] private GameObject _mineBlock;
        [SerializeField] private TextMeshPro _hintNumberText;

        public Spot spot { get; set; }
        public int IndexInGrid { get; private set; } = -1;

        private GridController _gridController;

        public void SetGridController(GridController controller) => _gridController = controller;
        public void SetIndexInGrid(int index) => IndexInGrid = index;

        public void Dig()
        {
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
                    SwtichToBlock(Block.Mine);
                    GameFinish.Raise(false);
                }
                else
                {
                    spot.State = SpotState.Dug;
                    SwtichToBlock(Block.Dug);
                    RevealHintNumber();
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

            void RevealHintNumber()
            {
                _hintNumberText.alpha = 1f;
                _hintNumberText.text = spot.HintNumber.ToString();
                switch (spot.HintNumber)
                {
                    case 1:
                        _hintNumberText.color = GameManager.Instance.CurrentLayout.One;
                        break;
                    case 2:
                        _hintNumberText.color = GameManager.Instance.CurrentLayout.Two;
                        break;
                    case 3:
                        _hintNumberText.color = GameManager.Instance.CurrentLayout.Three;
                        break;
                    case 4:
                        _hintNumberText.color = GameManager.Instance.CurrentLayout.Four;
                        break;
                    case 5:
                        _hintNumberText.color = GameManager.Instance.CurrentLayout.Five;
                        break;
                    case 6:
                        _hintNumberText.color = GameManager.Instance.CurrentLayout.Six;
                        break;
                    case 7:
                        _hintNumberText.color = GameManager.Instance.CurrentLayout.Seven;
                        break;
                    case 8:
                        _hintNumberText.color = GameManager.Instance.CurrentLayout.Eight;
                        break;
                    default:
                        _hintNumberText.alpha = 0f;
                        _hintNumberText.text = "";
                        break;
                }
            }
        }

        public void Mark()
        {
            if (spot.State == SpotState.Untouched)
            {
                // Don't allow for marking in the beginning of the game
                if (!_gridController.HasBegun)
                    return;

                spot.State = SpotState.Marked;
                SwtichToBlock(Block.Marked);
            }
            else if (spot.State == SpotState.Marked)
            {
                spot.State = SpotState.Untouched;
                SwtichToBlock(Block.Untouched);
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

        public async void OnSafeSpotDugAt(int index)
        {
            if (index == IndexInGrid)
            {
                //TODO: do the bounce animation and then swap to BugBlock
            }
        }

        public async void OnSpotMarkedAt(int index)
        {
            if (index == IndexInGrid)
            {
                //TODO: do the bounce animation and then swap to MarkedBlock
            }
        }

        private void Start()
        {
            transform.localScale = Vector3.one * GameManager.Instance.CurrentLayout.SpotSize;
            _hintNumberText.alpha = 0f;
            SwtichToBlock(Block.Untouched);
        }

        private void SwtichToBlock(Block block)
        {
            switch (block)
            {
                case Block.Untouched:
                    _untouchedBlock.SetActive(true);
                    _dugBlock.SetActive(false);
                    _markedBlock.SetActive(false);
                    _mineBlock.SetActive(false);
                    break;
                case Block.Dug:
                    _dugBlock.SetActive(true);
                    _untouchedBlock.SetActive(false);
                    _markedBlock.SetActive(false);
                    _mineBlock.SetActive(false);
                    break;
                case Block.Marked:
                    _markedBlock.SetActive(true);
                    _dugBlock.SetActive(false);
                    _untouchedBlock.SetActive(false);
                    _mineBlock.SetActive(false);
                    break;
                case Block.Mine:
                    _mineBlock.SetActive(true);
                    _dugBlock.SetActive(false);
                    _markedBlock.SetActive(false);
                    _untouchedBlock.SetActive(false);
                    break;
            }
        }

        private enum Block
        {
            Untouched,
            Dug,
            Marked,
            Mine
        }
    }
}
