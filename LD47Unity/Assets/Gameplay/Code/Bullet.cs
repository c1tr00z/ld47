using System;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class Bullet : MonoBehaviour {

        private Collider _collider;

        private bool _isLaunchable;

        public Collider collider => this.GetCachedComponent(ref _collider);

        private Rigidbody _rigidbody;

        public Rigidbody rigidbody => this.GetCachedComponent(ref _rigidbody);

        public bool isLaunchable {
            get => _isLaunchable;
            set {
                _isLaunchable = value;
                collider.enabled = !_isLaunchable;
                rigidbody.isKinematic = _isLaunchable;
                _visual.SetActive(!isLaunchable);
            }
        }

        public int damage = 2;

        [SerializeField] private GameObject _visual;

        private void OnCollisionEnter(Collision other) {
            var damageable = other.collider.GetColliderComponent<IDamageable>();
            if (damageable == null || damageable is ATM) {
                
            } else {
                damageable.Damage(damage);
            }
            
            isLaunchable = true;
        }

        public void Launch(float force) {
            rigidbody.velocity = transform.forward * force;
            isLaunchable = false;
        }
    }
}