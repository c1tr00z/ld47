using System;
using System.Collections.Generic;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class Player: Movable, IDamageable {

        #region Private Fields

        private ZombieRadar _zombieRadar;

        private float _lastAttackTime = 0;

        private List<IDamageable> _damageables = new List<IDamageable>();

        private Life _life;

        #endregion

        [SerializeField] private Shotgun _shotgun;

        [SerializeField] private float _attackDelay;

        [SerializeField] private ParticleSystem _explosion;

        #region Accessors

        public ZombieRadar zombieRadar => this.GetCachedComponentInChildren(ref _zombieRadar);
        
        public IZombie targetZombie { get; private set; }

        #endregion

        #region Movable Implementation

        protected override void LateUpdate() {

            base.LateUpdate();

            if (targetZombie == null || targetZombie.GetTransform() == null 
                                     || targetZombie.GetTransform().gameObject == null) {
                targetZombie = zombieRadar.GetNearest();
            }

            if (targetZombie == null || targetZombie.GetTransform() == null 
                                     || targetZombie.GetTransform().gameObject == null) {
                MoveTo(Vector3.zero);
                return;
            }

            var direction = transform.GetDirection(targetZombie.GetTransform());
            
            direction.y = 0;

            transform.rotation = Quaternion.LookRotation(direction);
            
            MoveTo(direction);
        }

        private void Update() {
            if (Time.time - _lastAttackTime >= _attackDelay) {
                Attack();
                _lastAttackTime = Time.time;
            }
        }

        #endregion

        #region Class Implementation

        private bool Attack() {

            var superNearest = zombieRadar.GetInRange(4);

            if (superNearest != null) {
                _explosion.Play();
                superNearest.ForEach(z => {
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
            
            _shotgun.Shoot();

            return true;
        }

        #endregion

        public Life GetLife() {
            return this.GetCachedComponent(ref _life);
        }
    }
}