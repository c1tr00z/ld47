using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace c1tr00z.ld47.Gameplay {
    [ExecuteInEditMode]
    public class ZombiesSpawner : MonoBehaviour {

        #region Private Fields

        private Vector2[,] _slots = new Vector2[0,0];

        private Vector2 _lastSize;
        
        private Vector2 _lastSlotSize;

        private Transform _transform;
        
        private bool[,] _spawnData = new bool[0,0];

        public List<Vector2Int> excludeSlots = new List<Vector2Int>();

        #endregion

        #region Private Fields

        [SerializeField] private Vector2 _size = Vector2.one;

        [SerializeField]
        private Vector2 _slotSize = Vector2.one;

        #endregion

        #region Accessors

        public Transform myTransform => this.GetCachedComponent(ref _transform);

        #endregion

        #region Unity Events

        private void Start() {
            RefreshSlots();
        }

        private void Update() {
#if UNITY_EDITOR
            if (!UnityEditor.EditorApplication.isPlaying) {
                RefreshSlots();
            }
#endif
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(myTransform.position, _size.ToVector3XZ());
            IterateSlots((x, y, point) => {
                Gizmos.DrawWireSphere(point, 1f);
            });
            Gizmos.color = Color.blue;
            excludeSlots.ForEach(es => {
                Gizmos.DrawWireSphere(transform.position + es.ToVector3().ToVector3XZ(), 1f);
            });
        }

        #endregion

        #region Class Implementation

        public void ClearSpawnData() {
            RefreshSlots();
            _spawnData = new bool[_slots.GetLength(0), _slots.GetLength(1)];
        }

        private void RefreshSlots() {

            if (_lastSize == _size && _lastSlotSize == _slotSize) {
                return;
            }
            
            var half = _size / 2;
            _slotSize.x = Mathf.Max(.01f, _slotSize.x);
            _slotSize.y = Mathf.Max(.01f, _slotSize.y);

            var deltaX = Mathf.CeilToInt(_slotSize.x);
            var deltaY = Mathf.CeilToInt(_slotSize.y);

            _slots = new Vector2[Mathf.CeilToInt(_size.x/deltaX),Mathf.CeilToInt(_size.y/deltaY)];
            for (var x = 0; x < _slots.GetLength(0); x += 1) {
                for (var y = 0; y < _slots.GetLength(1); y += 1) {
                    _slots[x, y] = new Vector2(x * deltaX - half.x + _slotSize.x / 2, y * deltaY - half.y + _slotSize.y / 2);
                }
            }

            _lastSlotSize = _slotSize;
            _lastSize = _size;
        }

        private void IterateSlots(Action<int, int, Vector3> iterator) {
            RefreshSlots();
            for (var x = 0; x < _slots.GetLength(0); x++) {
                for (var y = 0; y < _slots.GetLength(1); y++) {
                    iterator?.Invoke(x, y, GetSlotPosition(x, y));
                }
            }
        }

        public Vector3 GetSlotPosition(int x, int y) {
            RefreshSlots();
            if (x >= _slots.GetLength(0) || y >= _slots.GetLength(1)) {
                return Vector3.zero;
            }

            return _slots[x, y].ToVector3XZ() + myTransform.position;
        }

        public Vector3 GetRandomSlot() {

            var fits = false;
            Vector2Int randomSlot = new Vector2Int();

            var triesCount = 10;

            while (!fits && triesCount > 0) {
                triesCount--;
                
                randomSlot = new Vector2Int(Random.Range(0, _slots.GetLength(0)), 
                    Random.Range(0, _slots.GetLength(1)));

                if (_spawnData[randomSlot.x, randomSlot.x] || excludeSlots.Contains(randomSlot)) {
                    // Debug.LogError(randomSlot);
                } else {
                    _spawnData[randomSlot.x, randomSlot.x] = true;
                    fits = true;
                }
            }
            
            return GetSlotPosition(randomSlot.x, randomSlot.y);
        }

        public void SpawnAtRandom(Transform transform) {
            transform.position = GetRandomSlot();
        }

        #endregion
    }
}