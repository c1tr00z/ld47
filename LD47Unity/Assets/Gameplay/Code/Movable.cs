using c1tr00z.AssistLib.Sprites;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class Movable : MonoBehaviour {

        #region Private Fields

        private Rigidbody _rigidbody;

        private Vector3 _direction;

        private float _stunSeconds;

        private float _startStun;

        #endregion

        #region Serialized Fields

        [SerializeField] private SpriteAnimatorPrefixSetter _animatorPrefixSetter;

        [SerializeField] private float _speed = 1;

        #endregion

        #region Accessors

        public Rigidbody rigidbody => this.GetCachedComponent(ref _rigidbody);

        public bool isMoving { get; protected set; } = true;

        public SpriteAnimatorPrefixSetter animatorPrefixSetter => _animatorPrefixSetter;

        public bool isOtherAnimations { get; protected set; }

        #endregion
        
        #region Unity Event

        protected virtual void LateUpdate() {
            if (!isMoving) {
                return;
            }
            if (Time.time - _startStun < _stunSeconds) {
                return;
            }
            Move();
            RefreshMoveState();
        }

        #endregion

        #region Class Implementation

        private void Move() {
            var newVelocity = _direction * _speed;
            newVelocity.y = rigidbody.velocity.y;
            rigidbody.velocity = newVelocity;
        }

        protected void MoveTo(Vector3 direction) {
            _direction = direction;
        }

        private void RefreshMoveState() {
            if (isOtherAnimations) {
                return;
            }
            var state = "idle";
            if (rigidbody.velocity.magnitude > 0.1f) {
                state = "run";
            }
            _animatorPrefixSetter.PlayState(state);
        }

        public void Stun(float seconds) {
            _startStun = Time.time;
            _stunSeconds = seconds;
        }

        #endregion
    }
}