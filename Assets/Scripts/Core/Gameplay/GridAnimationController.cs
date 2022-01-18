using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Minesweeper.Animation;
using DG.Tweening;

namespace Minesweeper.Core
{
    public class GridAnimationController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] internal GridController gridController;
        [SerializeField] private Transform _camera;

        [Header("Animation")]
        [SerializeField] private bool _detonateAllMines = false;
        [SerializeField] private DetonateMines _detonateAllAnim;
        [SerializeField] private DetonateMines _detonateUnmarkedAnim;
        [SerializeField] private FloatAll _floatAllAnim;


        // Cache
        private Transform[] _spotTransforms;
        private Vector3[] _targetPositions;
        private SpotController[] _spotControllers;
        private Rigidbody[] _spotRBs;
        private Layout _layout;
        private int _gridSize;

        // Tweening
        private float _minMoveTime;
        private float _maxMoveTime;
        private Ease _easeMode;

        #region UNITY_METHODS
        private void Start()
        {
            _layout = GameManager.Instance.CurrentLayout;
            _gridSize = _layout.Width * _layout.Height;
            _spotTransforms = new Transform[_gridSize];
            _targetPositions = new Vector3[_gridSize];
            _spotControllers = new SpotController[_gridSize];
            _spotRBs = new Rigidbody[_gridSize];
            _minMoveTime = _layout.MinMoveTime;
            _maxMoveTime = _layout.MaxMoveTime;
            _easeMode = _layout.EaseMode;
        }
        #endregion
        #region INTERNAL_METHODS
        internal void SetTransformAt(int index, Transform t)
        {
            _spotTransforms[index] = t;
        }

        internal void SetTargetPositionAt(int index, Vector3 targetPosition)
        {
            _targetPositions[index] = targetPosition;
        }

        internal void SetSpotControllerAt(int index, SpotController sc)
        {
            _spotControllers[index] = sc;
        }

        internal void SetRigidBodyAt(int index, Rigidbody rb)
        {
            _spotRBs[index] = rb;
        }

        internal void SetAllIsKinematic(bool isKinematic)
        {
            foreach (var rb in _spotRBs)
                rb.isKinematic = isKinematic;
        }

        internal void SetAllUseGravity(bool useGravity)
        {
            foreach (var rb in _spotRBs)
                rb.useGravity = useGravity;
        }

        internal async Task MoveAllSpotsInPlace()
        {
            var positionTasks = new List<Task>();

            while (_spotTransforms == null)
            {
                await Task.Yield();
            }

            SetAllIsKinematic(true);
            SetAllUseGravity(false);

            for (int i = 0; i < _gridSize; i++)
            {
                Transform current = _spotTransforms[i];
                var task = current.DOMove(_targetPositions[i], Random.Range(_minMoveTime, _maxMoveTime)).SetEase(_easeMode).AsyncWaitForCompletion();
                positionTasks.Add(task);
            }
            await Task.WhenAll(positionTasks);
        }

        internal async Task MoveAllBack()
        {
            await MoveAllSpotsInPlace();

            var rotationTasks = new List<Task>();
            Vector3 forward = gridController.transform.forward * -1;
            foreach (var current in _spotTransforms)
            {
                var task = current.DORotate(forward, 0.2f).SetEase(Ease.Linear).AsyncWaitForCompletion();
                rotationTasks.Add(task);
            }
            await Task.WhenAll(rotationTasks);
        }

        internal async Task FloatAll()
        {
            SetAllIsKinematic(false);
            SetAllUseGravity(false);
            await _floatAllAnim.PerformAsync(null, _spotRBs, null, null, null);
            await Task.Delay(1_000);
        }

        internal async Task DetonateMines()
        {
            SetAllIsKinematic(false);
            SetAllUseGravity(false);

            if (_detonateAllMines)
            {
                var allMines = _spotControllers.Where(sc => sc.spot.IsMine);
                var rbs = allMines.Select(rb => rb.GetComponent<Rigidbody>());
                await _detonateAllAnim.PerformAsync(null, rbs, null, null, null);
            }
            else
            {
                var allUnmarkedMines = _spotControllers.Where(sc => sc.spot.IsMine && sc.spot.State != SpotState.Marked);
                var rbs = allUnmarkedMines.Select(rb => rb.GetComponent<Rigidbody>());
                await _detonateUnmarkedAnim.PerformAsync(null, rbs, null, null, null);
            }
        }

        internal async Task BounceAll(float inDuration, float outDuration, float delta, Ease inEase, Ease outEase)
        {
            List<Task> tasks = new List<Task>();
            float ogScaleFactor = _spotTransforms[0].localScale.x;
            float endValue = ogScaleFactor + delta;
            foreach (var t in _spotTransforms)
            {
                Sequence s = DOTween.Sequence();
                s.Append(t.DOScale(endValue, inDuration).SetEase(inEase));
                s.Append(t.DOScale(ogScaleFactor, outDuration).SetEase(outEase));
                var currentTask = s.AsyncWaitForCompletion();
                tasks.Add(currentTask);
            }

            await Task.WhenAll(tasks);
        }
        #endregion
    }
}