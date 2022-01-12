using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

namespace Minesweeper.Core
{
    public class GridAnimationController : MonoBehaviour
    {
        [SerializeField] internal GridController gridController;
        [SerializeField] private Transform _camera;

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
            foreach(var rb in _spotRBs)
                rb.isKinematic = isKinematic;
        }

        internal void SetAllUseGravity(bool useGravity)
        {
            foreach (var rb in _spotRBs)
                rb.useGravity = useGravity;
        }

        internal async Task MoveAllSpotsInPlace()
        {
            var tasks = new List<Task>();

            while (_spotTransforms == null)
            {
                await Task.Yield();
            }

            for (int i = 0; i < _gridSize; i++)
            {
                Transform current = _spotTransforms[i];
                var positionTask = current.DOMove(_targetPositions[i], Random.Range(_minMoveTime, _maxMoveTime)).SetEase(_easeMode).AsyncWaitForCompletion();
                tasks.Add(positionTask);
            }

            await Task.WhenAll(tasks);
        }

        internal async Task DropAllSpots()
        {
            await Task.Delay(200);
            for (int i = _gridSize - 1; i >= 0; i--)
            {
                _spotRBs[i].isKinematic = false;
                _spotRBs[i].useGravity = false;

                Vector3 rndDir = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
                Vector3 rndTorque = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
                _spotRBs[i].AddForce(rndDir, ForceMode.Impulse);
                _spotRBs[i].AddTorque(rndTorque, ForceMode.Impulse);
            }
            await Task.Delay(1000);
            foreach (Rigidbody rb in _spotRBs)
            {
                rb.useGravity = true;
            }
        }

        internal async Task DetonateAllMines()
        {
            await Task.Delay(200);

            foreach (var rb in _spotRBs)
            {
                rb.isKinematic = false;
                rb.useGravity = false;
            }

            // For each mine not makred
            var allUnmarkedMines = _spotControllers.Where(sc => sc.spot.IsMine && sc.spot.State != SpotState.Marked);

            foreach (var mine in allUnmarkedMines)
            {
                Rigidbody rb = mine.GetComponent<Rigidbody>();
                rb.AddExplosionForce(25f, rb.transform.position + Random.insideUnitSphere, 15f, 0f, ForceMode.Impulse);
                await Task.Delay(Random.Range(25, 50));
            }

        }
    }
}
