using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace c1tr00z.ld47.Gameplay {
    public class PunchZone : MonoBehaviour {

        [SerializeField] private UnityEvent _onPunchZoneEntered;
        
        private List<IDamageable> _damageables = new List<IDamageable>();

        private void OnTriggerEnter(Collider other) {
            _damageables = _damageables
                .Where(d => d != null && d.GetLife() != null 
                                      && d.GetLife().gameObject != null).ToList();
            var damageable = other.GetColliderComponent<IDamageable>();
            if (damageable == null) {
                return;
            }

            _damageables.Add(damageable);
            if (_onPunchZoneEntered != null) {
                _onPunchZoneEntered.Invoke();
            }
        }

        private void OnTriggerExit(Collider other) {
            _damageables = _damageables
                .Where(d => d != null && d.GetLife() != null 
                                      && d.GetLife().gameObject != null).ToList();
            
            var damageable = other.GetColliderComponent<IDamageable>();
            if (damageable == null) {
                return;
            }
            
            _damageables.Remove(damageable);
        }

        public List<IDamageable> GetAllInPunchZone() {
            _damageables = _damageables
                .Where(d => d != null && d.GetLife() != null 
                                      && d.GetLife().gameObject != null && d.GetLife().gameObject.activeSelf).ToList();
            return _damageables;
        }
    }
}