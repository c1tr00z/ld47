using System;
using c1tr00z.AssistLib.AppModules;
using TMPro;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class ATM : MonoBehaviour, IDamageable {

        [SerializeField] private TextMeshPro _coinsText;
        [SerializeField] private LifeBar3D _lifeBar;
        [SerializeField] private ParticleSystem _particleSystem;

        [SerializeField] private AudioSource _storeCoinSource;
        [SerializeField] private AudioSource _explosionSource;
        
        private Collider _collider;

        private Life _life;

        public Collider collider => this.GetCachedComponent(ref _collider);

        private Progression.Progression _progression;

        public Progression.Progression progression => ModulesUtils.GetCachedModule(ref _progression);
        
        public PlayerZombie playerZombie { get; private set; }

        private void Start() {
            collider.AddToColliders(this);
            OnDamage();
            ProgressionOnchanged();
        }

        private void OnEnable() {
            Progression.Progression.changed += ProgressionOnchanged;
        }

        private void OnDisable() {
            Progression.Progression.changed -= ProgressionOnchanged;
        }

        private void OnCollisionEnter(Collision other) {
            var otherCollider = other.collider;

            var coinBullet = otherCollider.GetColliderComponent<CoinBullet>();

            if (coinBullet != null) {
                progression.AddCoin();
                _storeCoinSource.Play();
            }
        }

        private void OnTriggerEnter(Collider other) {
            var player = other.GetColliderComponent<PlayerZombie>();
            if (player == null) {
                return;
            }

            playerZombie = player;
        }

        private void OnTriggerExit(Collider other) {
            var player = other.GetColliderComponent<PlayerZombie>();
            if (player == null) {
                return;
            }

            playerZombie = null;
        }

        private void ProgressionOnchanged() {
            _coinsText.text = progression.save.coins.ToString();
        }

        public Life GetLife() {
            return this.GetCachedComponent(ref _life);
        }

        public void OnDamage() {
            if (_lifeBar != null) {
                _lifeBar.SetValue(GetLife().lifeValue);
            }
        }

        public void Die() {
            if (!gameObject.activeSelf) {
                return;
            }

            _explosionSource.transform.parent = null;
            _explosionSource.Play();
            if (playerZombie != null) {
                playerZombie.AddCoins(progression.save.coins);
            }
            _particleSystem.transform.SetParent(null);
            _particleSystem.Play();
            gameObject.SetActive(false);
        }
    }
}