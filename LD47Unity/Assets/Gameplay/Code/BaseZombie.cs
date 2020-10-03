using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class BaseZombie : Movable, IZombie, IDamageable {

        #region Private Fields

        private Life _life;

        private Transform _transform;

        #endregion

        #region Serialized Fields

        [SerializeField]
        private PunchZone _punchZone;

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
            isMoving = false;
            Punch<IDamageable>();
        }

        public void Punch<T>() {
            var allFits = _punchZone.GetAllInPunchZone().OfType<T>();
            allFits.OfType<IDamageable>().ForEach(d => d.Damage(1));
            animatorPrefixSetter.PlayState("punch");
        }

        public virtual void OnEnterPunchZone() {
            
        }

        public void OnAnimationEvent(string eventName) {
            Debug.LogError(eventName);
            if (eventName == "punchEnded") {
                isMoving = true;
                Debug.LogError("Can move again");
            }
        }

        protected List<IDamageable> GetAllInPunchZone() {
            return _punchZone.GetAllInPunchZone();
        }

        #endregion
    }
}