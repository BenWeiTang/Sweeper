using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Minesweeper.Animation
{
    public class BlockPoolController : MonoBehaviour
    {
        [SerializeField] private int _count;
        [SerializeField, Range(0f, 2.5f)] private float _speed;
        [SerializeField, Range(0f, 2.5f)] private float _torque;
        [SerializeField, Range(0f, 20f)] private float _spawnSpereRadius = 10f;
        [SerializeField] private Transform _parent;
        [SerializeField] private List<GameObject> _blocksToSpawn = new List<GameObject>();
        [SerializeField] private PhysicMaterial _physicMaterial;
        [SerializeField] private float _explosionPeriod = 5f;
        [SerializeField, Range(0f, 10f)] private float _explosionForce = 2f;

        private ObjectPool<GameObject> _pool;
        private float _nextExplosionTime;
        private HashSet<Rigidbody> _blocksInUse = new HashSet<Rigidbody>();

        private void Awake()
        {
            _pool = new ObjectPool<GameObject>(CreateBlock, OnGetBlock, OnReleaseBlock, OnDestroyBlock, false, _count, _count * 10);
        }

        private void Start() 
        {
            SpawnBlocks();
            StartCoroutine(AddExplosion());

            IEnumerator AddExplosion()
            {
                while(true)
                {
                    if (Time.time > _nextExplosionTime)
                    {
                        _nextExplosionTime = Time.time + _explosionPeriod;
                        foreach(var rb in _blocksInUse)
                        {
                            rb.AddExplosionForce(_explosionForce, 
                                rb.transform.position + Random.onUnitSphere, 
                                10f, 0f, ForceMode.Impulse);
                        }
                    }
                    yield return null;
                }
            }
        }

        private void SpawnBlocks()
        {
            for (int i = 0; i < _count; i++)     
            {
                var go = _pool.Get();
            }
        }

        private GameObject CreateBlock()
        {
            if (_blocksToSpawn == null || _parent == null)
                return null;

            int rnd = UnityEngine.Random.Range(0, _blocksToSpawn.Count);
            var go = (GameObject)Instantiate(_blocksToSpawn[rnd], Random.insideUnitSphere * _spawnSpereRadius, Quaternion.identity);
            go.name = "Block Inactive";
            go.transform.SetParent(_parent);
            go.SetActive(false);

            var collider = go.AddComponent<BoxCollider>();
            collider.material = _physicMaterial;
            collider.material.dynamicFriction = 0;
            collider.material.bounciness = 1;

            var rb = go.AddComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = false;
            rb.drag = 0;
            rb.angularDrag = 0.05f;
            return go;
        }

        private void OnGetBlock(GameObject block)
        {
            block.name = "Block Active";
            block.SetActive(true);
            Rigidbody rb = block.GetComponent<Rigidbody>();
            rb.AddForce(Random.insideUnitSphere * _speed, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * _torque, ForceMode.Impulse);

            _blocksInUse.Add(rb);
        }
        private void OnReleaseBlock(GameObject block)
        {

            block.name = "Block Inactive";
            block.SetActive(false);
            Rigidbody rb = block.GetComponent<Rigidbody>();
            _blocksInUse.Remove(rb);
        }
        private void OnDestroyBlock(GameObject block)
        {
            Destroy(block);
        }
    }
}
