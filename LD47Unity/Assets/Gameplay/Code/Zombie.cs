using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace c1tr00z.ld47.Gameplay {
    public class Zombie : BaseZombie {

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

        private Vector3 _zombieDirection;

        private Collider _collider;

        #endregion

        #region Accessors

        public Collider collider => this.GetCachedComponent(ref _collider); 
        
        public Player player { get; private set; }

        #endregion

        #region Unity Events

        private void Awake() {
            collider.AddToColliders(this);
        }

        protected override void OnEnable() {
            base.OnEnable();
            GameplayController.playerSpawned += GameplayControllerOnplayerSpawned;
        }

        protected override void OnDisable() {
            base.OnDisable();
            GameplayController.playerSpawned -= GameplayControllerOnplayerSpawned;
        }

        private void OnDestroy() {
            collider.RemoveFromColliders();
        }

        #endregion

        #region Movable Implementation

        protected override void LateUpdate() {
            base.LateUpdate();

            if (player != null && GetTransform().GetDistance(player.transform) <= 10f) {
                GetTransform().rotation = Quaternion.LookRotation(GetTransform().GetDirection(player.transform));
                MoveTo(GetTransform().forward);
                return;
            }
            
            if (_currentAction == ZombieAction.Move) {
                MoveTo(_zombieDirection);
            } else {
                MoveTo(Vector3.zero);
            }

            if (Time.time - _actionStartedTime >= _actionTime) {
                _actionStartedTime = Time.time;
                _currentAction = Random.Range(0f, 1f) > 0.7f ? ZombieAction.Move : ZombieAction.Idle;
                if (_currentAction == ZombieAction.Move) {
                    _zombieDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
                    _actionTime = Random.Range(1f, 10f);
                } else {
                    _actionTime = Random.Range(5f, 30f);
                }
            }
        }
        
        #endregion

        #region Class Implementation

        public void Die() {
            Destroy(gameObject);
        }

        private void GameplayControllerOnplayerSpawned(Player player) {
            this.player = player;
        }

        #endregion

        public override void OnEnterPunchZone() {
            if (GetAllInPunchZone().OfType<Player>().Any()) {
                Punch<Player>();
            }
        }
    }
}