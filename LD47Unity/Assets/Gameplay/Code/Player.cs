using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace c1tr00z.ld47.Gameplay {
    public class Player: Movable, IDamageable {

        #region Events

        public static event Action changed;

        #endregion

        #region Private Fields

        private ZombieRadar _zombieRadar;

        private float _lastAttackTime = 0;

        private List<IDamageable> _damageables = new List<IDamageable>();

        private Life _life;

        private Collider _collider;

        private float _forwardCheckTime = 0;

        private float _forwardCheckCooldown = 3;

        private bool _back;

        private Vector3 _additionalDirection;

        #endregion

        [SerializeField] private Shotgun _shotgun;

        [SerializeField] private float _attackDelay;

        [SerializeField] private ParticleSystem _explosion;

        #region Accessors

        public ZombieRadar zombieRadar => this.GetCachedComponentInChildren(ref _zombieRadar);
        
        public IZombie targetZombie { get; private set; }

        public Collider collider => this.GetCachedComponent(ref _collider);
        
        public int exp { get; private set; }

        public int level => Mathf.Min(5, (exp / maxExp) + 1);
        
        public int maxExp => 10;

        public Vector3 targetDirection;

        #endregion

        #region Movable Implementation

        private void Awake() {
            collider.AddToColliders(this);
        }

        protected override void LateUpdate() {

            base.LateUpdate();

            if (targetZombie == null || targetZombie.GetTransform() == null 
                                     || targetZombie.GetTransform().gameObject == null || targetZombie.ToString() == "null") {
                targetZombie = zombieRadar.GetNearest();
            }

            if (targetZombie == null || targetZombie.GetTransform() == null 
                                     || targetZombie.GetTransform().gameObject == null || targetZombie.ToString() == "null") {
                MoveTo(Vector3.zero);
                return;
            }

            targetDirection = transform.GetDirection(targetZombie.GetTransform());
            
            targetDirection.y = 0;

            transform.rotation = Quaternion.LookRotation(targetDirection);

            if (Time.time - _forwardCheckTime >= _forwardCheckCooldown) {
                
                var raycastMask = 1 << 12;
                if (Physics.Raycast(transform.position, transform.forward, 4, raycastMask)) {
                    _back = true;
                    Debug.LogError("Back");
                    _additionalDirection = transform.right * (UnityEngine.Random.Range(0, 2) == 0 ? 1 : -1);
                } else {
                    _back = false;
                    _additionalDirection = Vector3.zero;
                }

                _forwardCheckTime = Time.time;
            }

            if (_back) {
                targetDirection = targetDirection * -1 + _additionalDirection;
            }
            
            MoveTo(targetDirection);
        }

        private void Update() {

            if (!GetLife().isAlive) {
                return;
            }

            if (rigidbody.velocity.magnitude < 1) {
                MoveTo(transform.forward * -1);
            }
            
            if (Time.time - _lastAttackTime >= _attackDelay) {
                Attack();
                _lastAttackTime = Time.time;
            }
            _shotgun.Shoot();
        }

        private void OnDestroy() {
            collider.RemoveFromColliders();
        }

        #endregion

        #region Class Implementation

        private bool Attack() {

            var superNearest = zombieRadar.GetInRange(3);

            if (superNearest != null) {
                _explosion.Play();
                superNearest.Where(z => z != null && z.GetRigidbody() != null).ForEach(z => {
                    z.GetRigidbody().AddForce(transform.GetDirection(z.GetTransform()) * 5000);
                    if (z is IDamageable damageable) {
                        damageable.Damage(2);
                    }

                    if (z is Movable m) {
                        m.Stun(1);
                    }
                });
            }
            
            if (targetZombie == null) {
                return false;
            }

            return true;
        }

        #endregion

        public Life GetLife() {
            return this.GetCachedComponent(ref _life);
        }

        public void OnCollectExp() {
            exp += 1;
            _shotgun.level = level;
            changed?.Invoke();
        }

        public void ShowDeath() {
            _explosion.Play();
            animatorPrefixSetter.spriteAnimator.gameObject.SetActive(false);
        }

        public void OnDie() {
            
        }
    }
}