using System;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class CoinBullet : MonoBehaviour {
        private Rigidbody _rigidbody;

        private bool _launchable = false;

        public Rigidbody rigidbody => this.GetCachedComponent(ref _rigidbody);

        public bool isLaunchable {
            get => _launchable;
            set {
                _launchable = value;
                rigidbody.isKinematic = _launchable;
                _visual.SetActive(!_launchable);
                collider.enabled = !_launchable;
            }
        }

        public int damage = 2;

        public Coin coinSrc;

        [SerializeField] private GameObject _visual;

        private Collider _collider;

        public Collider collider => this.GetCachedComponent(ref _collider);

        private void Awake() {
            collider.AddToColliders(this);
        }

        private void OnCollisionEnter(Collision other) {

            if (isLaunchable) {
                return;
            }

            var atm = other.collider.GetColliderComponent<ATM>();

            if (atm == null) {
                coinSrc.Clone().transform.position = transform.position;
            }

            isLaunchable = true;
        }

        public void Launch(float force) {
            rigidbody.velocity = transform.forward * force;
            isLaunchable = false;
        }
    }
}