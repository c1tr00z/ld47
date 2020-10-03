using System;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class Bullet : MonoBehaviour {

        private void Start() {
            GetComponent<Rigidbody>().velocity = transform.forward * 20;
        }

        private void OnCollisionEnter(Collision other) {
            var damageable = other.collider.GetColliderComponent<IDamageable>();
            if (damageable == null) {
                
            } else {
                damageable.Damage(2);
            }
            
            Destroy(gameObject);
            
        }
    }
}