using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.ResourcesManagement;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class PlayerZombie : BaseZombie {

        #region Events

        public static event Action changed;

        #endregion

        #region Private Fields

        private Collider _collider;

        private List<CoinBullet> _coinBullets = new List<CoinBullet>();

        private GameplayController _gameplayController;

        #endregion

        #region Serialized Fields

        [SerializeField] private KeyCode _upKey;
        
        [SerializeField] private KeyCode _downKey;
        
        [SerializeField] private KeyCode _rightKey;
        
        [SerializeField] private KeyCode _leftKey;
        
        [SerializeField] private CoinBullet _coinBulletSrc;
        
        [SerializeField] private Transform _coinShootingPoint;

        #endregion
        
        #region Accessors

        public Collider collider => this.GetCachedComponent(ref _collider);
        
        public int exp { get; private set; }

        public int level => Mathf.Min(5, (exp / maxExp) + 1);
        
        public int maxExp => 10;
        
        public Shotgun shotgun { get; private set; }
        
        public int coins { get; private set; }

        #endregion

        public override int damage => level;

        private void Awake() {
            collider.AddToColliders(this);
        }

        private void Update() {

            if (!GetLife().isAlive) {
                return;
            }
            
            var direction = new Vector3();
            
            if (Input.GetKey(_upKey)) {
                direction.z += 1;
            }
            if (Input.GetKey(_downKey)) {
                direction.z -= 1;
            }
            if (Input.GetKey(_rightKey)) {
                direction.x += 1;
            }
            if (Input.GetKey(_leftKey)) {
                direction.x -= 1;
            }

            if (Input.GetMouseButtonDown(0)) {
                Punch();
            } else if (Input.GetMouseButtonDown(1)) {
                Shoot();
            }

            MoveTo(direction);
        }

        private void OnDestroy() {
            collider.RemoveFromColliders();
        }

        public void Shoot() {
            if (shotgun != null) {
                shotgun.Shoot();
                return;
            }

            TossCoin();
        }

        private void TossCoin() {
            if (coins <= 0) {
                return;
            }
            coins--;
            var bullet = _coinBullets.FirstOrDefault(b => b.isLaunchable);
            if (bullet == null) {
                bullet = _coinBulletSrc.Clone();
                _coinBullets.Add(bullet);
            }

            var bulletTransform = bullet.transform;
            bulletTransform.position = _coinShootingPoint.transform.position;
            bulletTransform.rotation = Quaternion.LookRotation(GetTransform().forward);
            bullet.Launch(20);
            changed?.Invoke();
        }

        public void AddShotgun() {
            shotgun = DB.Get<ShotgunDBEntry>().LoadPrefab<Shotgun>().Clone();
            shotgun.Reset(GetTransform());
            changed?.Invoke();
        }

        public void OnExpCollected() {
            exp++;
            if (shotgun != null) {
                shotgun.level = level;
            }
            GetLife().SetDefaultLives(level * 5);
            changed?.Invoke();
        }

        public bool GetCoins(int coinsToGet) {
            if (coins < coinsToGet) {
                return false;
            }

            coins -= coinsToGet;
            changed?.Invoke();
            return true;
        }

        public void AddCoins(int coinsToAdd) {
            coins += coinsToAdd;
            changed?.Invoke();
        }
        
        public void OnCoinCollected() {
            AddCoins(1);
        }
    }
}