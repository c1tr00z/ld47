using System;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class PlayerZombie : Movable, IZombie {

        #region Private Fields

        private Transform _transform;

        private Collider _collider;

        #endregion

        #region Serialized Fields

        [SerializeField] private KeyCode _upKey;
        
        [SerializeField] private KeyCode _downKey;
        
        [SerializeField] private KeyCode _rightKey;
        
        [SerializeField] private KeyCode _leftKey;

        #endregion
        
        #region Accessors

        public Collider collider => this.GetCachedComponent(ref _collider);

        #endregion

        private void Awake() {
            collider.AddToColliders(this);
        }

        private void Update() {
            
            var direction = new Vector3();
            
            if (Input.GetKey(_upKey)) {
                direction.z += 1;
            }
            if (Input.GetKey(_downKey)) {
                direction.z -= 1;
            }
            if (Input.GetKey(_rightKey)) {
                direction.x += 1;
            }
            if (Input.GetKey(_leftKey)) {
                direction.x -= 1;
            }

            MoveTo(direction);
        }

        private void OnDestroy() {
            collider.RemoveFromColliders();
        }

        #region IZombie Implementation

        public Transform GetTransform() {
            return this.GetCachedComponent(ref _transform);
        }

        #endregion
    }
}