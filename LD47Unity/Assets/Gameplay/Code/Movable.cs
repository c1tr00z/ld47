using c1tr00z.AssistLib.Sprites;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class Movable : MonoBehaviour {

        #region Private Fields

        private Rigidbody _rigidbody;

        private Vector3 _direction;

        #endregion

        #region Serialized Fields

        [SerializeField] private SpriteAnimatorPrefixSetter _animatorPrefixSetter;

        [SerializeField] private float _speed = 1;

        #endregion

        #region Accessors

        public Rigidbody rigidbody => this.GetCachedComponent(ref _rigidbody);

        #endregion
        
        #region Unity Event

        private void LateUpdate() {
            Move();
            RefreshMoveState();
        }

        #endregion

        #region Class Implementation

        private void Move() {
            rigidbody.velocity = _direction * _speed;
        }

        protected void MoveTo(Vector3 direction) {
            _direction = direction;
        }

        private void RefreshMoveState() {
            var state = "idle";
            if (rigidbody.velocity.magnitude > 0.1f) {
                state = "run";
            }
            _animatorPrefixSetter.PlayState(state);
        }

        #endregion
    }
}