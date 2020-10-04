using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class BaseZombie : Movable, IZombie, IDamageable {

        #region Private Fields

        private Life _life;

        private Transform _transform;

        private float _lastPunchTime;

        #endregion

        #region Serialized Fields

        [SerializeField]
        private PunchZone _punchZone;

        [SerializeField] private float _punchCooldown;

        [SerializeField] private int _damage = 1;

        [SerializeField] private AudioSource _hurt;

        #endregion

        #region Accessors

        public virtual int damage => _damage;

        #endregion

        #region Unity Events

        protected virtual void OnEnable() {
            animatorPrefixSetter.spriteAnimator.OnStateEvent += OnAnimationEvent;
        }

        protected virtual void OnDisable() {
            animatorPrefixSetter.spriteAnimator.OnStateEvent -= OnAnimationEvent;
        }

        #endregion

        #region IZombie Implementation

        public Transform GetTransform() {
            return this.GetCachedComponent(ref _transform);
        }

        public Rigidbody GetRigidbody() {
            return rigidbody;
        }

        #endregion

        #region IDamageable Implementation

        public Life GetLife() {
            return this.GetCachedComponent(ref _life);
        }

        public void Punch() {
            if (Time.time - _lastPunchTime < _punchCooldown) {
                return;
            }

            _lastPunchTime = Time.time;
            isMoving = false;
            animatorPrefixSetter.PlayState("punch");
        }

        public virtual void OnEnterPunchZone() {
            
        }

        public virtual List<IDamageable> SelectPuncheables(List<IDamageable> damageables) {
            return damageables;
        }

        private void PerformDamage(List<IDamageable> damageables) {
            if (_hurt != null) {
                _hurt.Play();
            }
            damageables.OfType<IDamageable>().ForEach(d => d.Damage(damage));
        }

        public void OnAnimationEvent(string eventName) {
            if (eventName == "punchEnded") {
                isMoving = true;;
                return;
            }
            if (eventName == "punch") {
                PerformDamage(SelectPuncheables(_punchZone.GetAllInPunchZone()));
            }
        }

        protected List<IDamageable> GetAllInPunchZone() {
            return _punchZone.GetAllInPunchZone();
        }

        public virtual void OnDamage() {
        }

        #endregion
    }
}