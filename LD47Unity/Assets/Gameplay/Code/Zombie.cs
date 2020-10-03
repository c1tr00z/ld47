using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace c1tr00z.ld47.Gameplay {
    public class Zombie : Movable, IZombie, IDamageable {

        #region Enums

        private enum ZombieAction {
            Move,
            Idle,
        }

        #endregion

        #region Private Fields

        private ZombieAction _currentAction = ZombieAction.Idle;

        private float _actionTime;

        private float _actionStartedTime;

        private Vector3 _direction;

        private Transform _transform;

        private Life _life;

        private Collider _collider;

        #endregion

        #region Accessors

        public Collider collider => this.GetCachedComponent(ref _collider); 

        #endregion

        #region Unity Events

        private void Awake() {
            collider.AddToColliders(this);
        }

        private void OnDestroy() {
            collider.RemoveFromColliders();
        }

        #endregion

        #region Movable Implementation

        protected override void LateUpdate() {
            base.LateUpdate();
            if (_currentAction == ZombieAction.Move) {
                MoveTo(_direction);
            } else {
                MoveTo(Vector3.zero);
            }

            if (Time.time - _actionStartedTime >= _actionTime) {
                _actionStartedTime = Time.time;
                _currentAction = Random.Range(0f, 1f) > 0.7f ? ZombieAction.Move : ZombieAction.Idle;
                if (_currentAction == ZombieAction.Move) {
                    _direction = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                    _actionTime = Random.Range(1f, 10f);
                } else {
                    _actionTime = Random.Range(5f, 30f);
                }
            }
        }
        
        #endregion
        
        #region IZombie Implementation

        public Transform GetTransform() {
            return this.GetCachedComponent(ref _transform);
        }

        #endregion

        #region IDamageable Implementation

        public Life GetLife() {
            return this.GetCachedComponent(ref _life);
        }

        #endregion
    }
}