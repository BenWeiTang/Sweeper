using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using Minesweeper.Animation;

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
        [SerializeField] private MoveAllTo _moveAllInPlaceAnim;
        [SerializeField] private RotateAllTowards _lookForwardAnim;
        [SerializeField] private BounceAll _bounceAllAnim;


        // Cache
        private Transform[] _spotTransforms;
        private Vector3[] _targetPositions;
        private SpotController[] _spotControllers;
        private Rigidbody[] _spotRBs;
        private Layout _layout;
        private int _gridSize;

        #region UNITY_METHODS
        private void Start()
        {
            _layout = GameManager.Instance.CurrentLayout;
            _gridSize = _layout.Width * _layout.Height;
            _spotTransforms = new Transform[_gridSize];
            _targetPositions = new Vector3[_gridSize];
            _spotControllers = new SpotController[_gridSize];
            _spotRBs = new Rigidbody[_gridSize];
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
            SetAllIsKinematic(true);
            SetAllUseGravity(false);

            await _moveAllInPlaceAnim.PerformAsync(_spotTransforms, _targetPositions);
        }

        internal async Task MoveAllBack()
        {
            SetAllIsKinematic(true);
            SetAllUseGravity(false);

            await _moveAllInPlaceAnim.PerformAsync(_spotTransforms, _targetPositions);

            Vector3 forward =  gridController.transform.forward * -1; 
            await _lookForwardAnim.PerformAsync(_spotTransforms, forward);
        }

        internal async Task FloatAll()
        {
            SetAllIsKinematic(false);
            SetAllUseGravity(false);
            await _floatAllAnim.PerformAsync(_spotRBs);
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
                await _detonateAllAnim.PerformAsync(rbs);
            }
            else
            {
                var allUnmarkedMines = _spotControllers.Where(sc => sc.spot.IsMine && sc.spot.State != SpotState.Marked);
                var rbs = allUnmarkedMines.Select(rb => rb.GetComponent<Rigidbody>());
                await _detonateUnmarkedAnim.PerformAsync(rbs);
            }
        }

        internal async Task BounceAll()
        {
            await _bounceAllAnim.PerformAsync(_spotTransforms);
        }
        #endregion
    }
}