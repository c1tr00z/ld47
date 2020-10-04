using System;
using System.Collections.Generic;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class BloodJar : MonoBehaviour, IDamageable {

        private Life _life;
        
        private List<BaseZombie> _baseZombies = new List<BaseZombie>();

        public Color bloodColor = Color.red;

        public ParticleSystem particles;

        private Collider _collider;

        public Collider collider => this.GetCachedComponent(ref _collider);

        private void Start() {
            collider.AddToColliders(this);
        }

        public Life GetLife() {
            return this.GetCachedComponent(ref _life);
        }

        private void OnTriggerEnter(Collider other) {
            var zombie = other.GetColliderComponent<BaseZombie>();

            if (zombie == null || _baseZombies.Contains(zombie)) {
                return;
            }
            
            _baseZombies.Add(zombie);
        }

        private void OnTriggerExit(Collider other) {
            var zombie = other.GetColliderComponent<BaseZombie>();

            if (zombie == null || !_baseZombies.Contains(zombie)) {
                return;
            }
            
            _baseZombies.Remove(zombie);
        }

        public void OnDie() {
            _baseZombies = _baseZombies.Where(z => z != null && z.gameObject != null).ToList();
            particles.transform.parent = null;
            particles.Play();
            _baseZombies.ForEach(z => z.animatorPrefixSetter.spriteAnimator.spriteRenderer.color = bloodColor);
            gameObject.SetActive(false);
        }

    }
}