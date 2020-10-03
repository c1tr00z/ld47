using System;
using System.Collections.Generic;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class ZombieRadar : MonoBehaviour {

        #region Private Fields
        
        private List<IZombie> _zombies = new List<IZombie>();

        #endregion

        #region Unity Events

        private void OnEnable() {
            Life.died += LifeOnDied;
        }

        private void OnDisable() {
            Life.died -= LifeOnDied;
        }

        private void OnTriggerEnter(Collider other) {
            var zombie = other.GetColliderComponent<IZombie>();

            if (zombie == null) {
                return;
            }
            
            _zombies.Add(zombie);
        }

        private void OnTriggerExit(Collider other) {
            CheckCollider(other);
        }

        private void OnTriggerStay(Collider other) {
            CheckCollider(other);
        }

        #endregion

        #region Class Implementation

        private void CheckCollider(Collider other) {
            var zombie = other.GetColliderComponent<IZombie>();

            if (zombie == null) {
                return;
            }

            if (_zombies.Contains(zombie)) {
                return;
            }
            _zombies.Remove(zombie);
        }

        private void LifeOnDied(Life life) {
            var zombie = life.GetLifeComponent<IZombie>();
            if (zombie == null) {
                return;
            }

            _zombies.Remove(zombie);
        }

        public IZombie GetNearest() {
            return _zombies.MinElement(z => transform.GetDistance(z.GetTransform()));
        }

        public List<IZombie> GetInRange(float range) {
            var result = _zombies.Where(z => transform.GetDistance(z.GetTransform()) <= range).ToList();
            return result;
        }

        #endregion
    }
}