using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using Minesweeper.Event;
using Minesweeper.Reference;

namespace Minesweeper.Core
{
    public class GridController : MonoBehaviour
    {
        [SerializeField] private Transform _camera;

        [Header("Event")]
        [SerializeField] private VoidEvent GameReady;
        [SerializeField] private BoolEvent GameFinish;
        [SerializeField] private BoolEvent PostGameExit;

        [Header("Animation")]
        [SerializeField] internal GridAnimationController animationController;

        [Header("Reference")]
        [SerializeField] private IntRef _safeSpotCount;

        public bool HasBegun { get; set; } = false;

        private Layout _layout;
        private Vector3 _displacement;
        private SpotController _spotPrefab;
        private float _spawSphereRadius = 10f;
        private int _gridSize;
        private Spot[] _grid;
        private SpotController[] _spotControllers;
        private int _dugSafeSpotCount = 0;

        #region PUBLIC_METHODS
        public IEnumerable<SpotController> GetAdjacentSpotControllers(SpotController controller)
        {
            int index = System.Array.IndexOf(_spotControllers, controller);

            foreach (int ai in GetAdjacentIndices(index))
            {
                yield return _spotControllers[ai];
            }
        }

        public IEnumerable<SpotController> GetAllSafeSpots() => _spotControllers.Where(s => !s.spot.IsMine);

        #endregion
        #region PUBLIC_CALLBACKS
        public async void OnFadeBlindOutCompleted()
        {
            await animationController.MoveAllSpotsInPlace();
            await animationController.BounceAll();
            GameReady.Raise();
        }

        public void OnFirstSafeSpotDug()
        {
            for (int i = 0; i < _gridSize; i++)
            {
                int sum = 0;
                foreach (int ai in GetAdjacentIndices(i))
                {
                    sum = _grid[ai].IsMine ? sum + 1 : sum;
                }
                _grid[i].HintNumber = _grid[i].IsMine ? -1 : sum;
            }

            _safeSpotCount.value = _grid.Count(s => s.IsMine == false);
        }

        public void OnSafeSpotDug(int hintNumber)
        {
            _dugSafeSpotCount++;
            if (_dugSafeSpotCount == _safeSpotCount.value)
            {
                GameFinish.Raise(true);
            }
        }

        public async void OnPostGameEnter(bool won)
        {
            if (won)
            {
                await Task.Delay(200);
                await animationController.FloatAll();
            }
            else
            {
                await Task.Delay(300);
                await animationController.DetonateMines();
            }

            // UI Manager listens to this
            PostGameExit.Raise(won);
        }

        public async void OnGameRestart(bool shouldMoveBlocks)
        {
            if (shouldMoveBlocks)
            {
                await animationController.MoveAllBack();
            }
            await Task.Delay(100);

            // Clear mines, set state to untouched, swtich block to untouched while doing ShakeToBlock anim
            List<Task> resettingTasks = new List<Task>();
            foreach (var sc in _spotControllers)
            {
                resettingTasks.Add(sc.ResetSpot());
            }
            await Task.WhenAll(resettingTasks);
            await Task.Delay(100);

            await animationController.BounceAll();

            _dugSafeSpotCount = 0;
            RandomGenerateMines();
            HasBegun = false;
            GameReady.Raise();
        }
        #endregion
        #region UNITY_METHODS
        private void Awake()
        {
            if (!_camera) { _camera = Camera.main.transform; }
        }

        private void Start()
        {
            LayoutInit();
            MoveGridToCamera();
            GridInit();
            GenerateGrid();
        }
        #endregion
        #region PRIVATE_METHODS
        private void LayoutInit()
        {
            _layout = GameManager.Instance.CurrentLayout;
            _displacement = _layout.CameraDisplacement;
            _spotPrefab = _layout.SpotPrefab;
            _spawSphereRadius = _layout.SpawnSphereRadius;
        }

        private void GridInit()
        {
            _grid = new Spot[_layout.Width * _layout.Height];
            _gridSize = _grid.Length;
            RandomGenerateMines();
            _spotControllers = new SpotController[_gridSize]; // solely for use of GetAdjacent;
        }

        private void RandomGenerateMines()
        {
            for (int i = 0; i < _gridSize; i++)
            {
                if (_grid[i] == null)
                {
                    _grid[i] = new Spot();
                }
                else
                {
                    _grid[i].HintNumber = 0;
                }
                bool isMine = Random.Range(0f, 100f) < _layout.MinePercentage;
                _grid[i].SetMine(isMine);
            }
        }

        private void GenerateGrid()
        {
            float totalWidth = _layout.SpotSize * _layout.Width + _layout.Spacing * (_layout.Width - 1);
            float x0 = (totalWidth * 0.5f - _layout.SpotSize * 0.5f) * -1;

            float totalHeigh = _layout.SpotSize * _layout.Height + _layout.Spacing * (_layout.Height - 1);
            float y0 = (totalHeigh * 0.5f - _layout.SpotSize * 0.5f);

            Vector3 initialPos = new Vector3(x0 + transform.position.x, y0 + transform.position.y, 0f);
            initialPos += _displacement;

            for (int i = 0; i < _gridSize; i++)
            {
                float horizontalPosDiff = (i % _layout.Width) * (_layout.SpotSize + _layout.Spacing);
                float verticalPosDiff = Mathf.RoundToInt(i / _layout.Width) * (_layout.SpotSize + _layout.Spacing) * -1;
                Vector3 newPos = new Vector3(horizontalPosDiff, verticalPosDiff, 0f);
                newPos = newPos + initialPos;

                Vector3 randomPos = Random.insideUnitSphere * _spawSphereRadius + transform.position;
                var spotController = Instantiate(_spotPrefab, randomPos, Quaternion.identity);
                spotController.transform.SetParent(transform);
                spotController.SetGridController(this);
                spotController.spot = _grid[i];

                _spotControllers[i] = spotController;

                spotController.SetIndexInGrid(i);
                animationController.SetSpotControllerAt(i, spotController);
                animationController.SetTargetPositionAt(i, newPos);
                animationController.SetTransformAt(i, spotController.transform);
                animationController.SetRigidBodyAt(i, spotController.GetComponent<Rigidbody>());
            }
        }

        private void MoveGrid(Vector3 position, Vector3 direction)
        {
            transform.position = position;
            transform.forward = direction;
        }

        private void MoveGridToCamera()
        {
            MoveGrid(_camera.position + _displacement, _camera.forward);
        }

        private IEnumerable<int> GetAdjacentIndices(int index)
        {
            bool isTop = false;
            bool isBottom = false;
            bool isLeft = false;
            bool isRight = false;

            isTop = index / _layout.Width == 0;
            if (!isTop)
                isBottom = index / _layout.Width == _layout.Height - 1;

            isLeft = index % _layout.Width == 0;
            if (!isLeft)
                isRight = index % _layout.Width == _layout.Width - 1;

            if (!isTop && !isLeft)
                yield return index - _layout.Width - 1; // up left

            if (!isTop)
                yield return index - _layout.Width;     // up 

            if (!isTop && !isRight)
                yield return index - _layout.Width + 1; // up right 

            if (!isLeft)
                yield return index - 1;                 // left

            if (!isRight)
                yield return index + 1;                 // right

            if (!isBottom && !isLeft)
                yield return index + _layout.Width - 1; // down left

            if (!isBottom)
                yield return index + _layout.Width;     // down

            if (!isBottom && !isRight)
                yield return index + _layout.Width + 1; // down right
        }
        #endregion

#if UNITY_EDITOR
        [ContextMenu("Show Everything")]
        private void ShowEverything()
        {
            foreach (var spotController in _spotControllers)
            {
                spotController.ShowSpot();
            }
        }

        [ContextMenu("Show Mines")]
        private void ShowMines()
        {
            foreach (var spotController in _spotControllers.Where(s => s.spot.IsMine))
            {
                spotController.ShowSpot();
            }
        }

        [ContextMenu("Show Everything Except Mines")]
        private void ShowEverythingExceptMines()
        {
            foreach (var spotController in _spotControllers.Where(s => !s.spot.IsMine))
            {
                spotController.ShowSpot();
            }
        }

        [ContextMenu("Hide Everything")]
        private void HideEverything()
        {
            foreach (var spotController in _spotControllers.Where(s => s.spot.State != SpotState.Dug))
            {
                spotController.HideSpot();
            }
        }
#endif
    }
}