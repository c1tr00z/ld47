using System;
using System.Collections;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.ResourcesManagement;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class GameplayController : Module {

        #region Events

        public static event Action<PlayerZombie> playerZombieSpawned;
        
        public static event Action<Player> playerSpawned;

        public static event Action Changed;

        public static event Action playerDied;

        #endregion

        #region Private Fields

        private float _startPlayerTime;

        private bool _playerTimeStarted;

        #endregion

        #region Serialized Fields

        [SerializeField] private Room _room;

        [SerializeField] private int _zombieCount;

        [SerializeField] private int _secondsBeforePlayer;

        [SerializeField] private UIFrameDBEntry _playFrame;
        
        [SerializeField] private UIFrameDBEntry _gameOverFrame;
        
        [SerializeField] private UIFrameDBEntry _outOfLoopFrame;

        [SerializeField] private SelfDestructParticles _zombieExplosion;
        
        [SerializeField] private Coin _coinSrc;
        
        [SerializeField] private Exp _expSrc;

        #endregion

        #region Accessors

        public PlayerZombie playerZombie { get; private set; }
        
        public Player player { get; private set; }
        
        public int secondsBeforePlayer { get; private set; }

        #endregion

        #region Unity Events

        private void OnEnable() {
            Life.died += LifeOnDied;
        }

        private void OnDisable() {
            Life.died -= LifeOnDied;
        }

        private void Start() {
            _playFrame.Show();
            SpawnZombies();
            PlayerTimeStart();
        }

        private void Update() {
            PlayerCooldown();
        }

        #endregion

        #region Class Implementation

        private void SpawnZombies() {
            _room.spawner.ClearSpawnData();
            var zombiesDBEntries = DB.GetAll<ZombieDBEntry>();
            new[] { _zombieCount }.Iterate(i => {
                _room.spawner.SpawnAtRandom(zombiesDBEntries.RandomItem().LoadPrefab<Zombie>().Clone().transform);
            });

            playerZombie = DB.Get<PlayerZombieDBEntry>().LoadPrefab<PlayerZombie>().Clone();
            
            _room.spawner.SpawnAtRandom(playerZombie.transform);
            
            playerZombieSpawned?.Invoke(playerZombie);
        }

        public void PlayerTimeStart() {
            _playerTimeStarted = true;
            _startPlayerTime = Time.time;
        }

        private void PlayerCooldown() {

            if (player != null) {
                return;
            }
            
            if (!_playerTimeStarted) {
                return;
            }

            var seconds = Mathf.FloorToInt(_secondsBeforePlayer - (Time.time - _startPlayerTime));

            while ((Time.time - _startPlayerTime) < _secondsBeforePlayer) {
                secondsBeforePlayer = seconds;
                Changed?.Invoke();
                return;
            }
            

            secondsBeforePlayer = 0;
            
            Changed?.Invoke();
            
            SpawnPlayer();
        }

        private void SpawnPlayer() {
            player = DB.Get<PlayerDBEntry>().LoadPrefab<Player>().Clone();
            player.transform.position = _room.playerSpawnPoint;
            playerSpawned?.Invoke(player);
        }

        private void LifeOnDied(Life life) {
            var player = life.GetLifeComponent<Player>();
            if (player != null) {
                playerDied?.Invoke();
                StartCoroutine(C_Win());
                return;
            }

            var playerZombie = life.GetLifeComponent<PlayerZombie>();
            if (playerZombie != null) {
                _gameOverFrame.Show();
                return;
            }

            var zombie = life.GetLifeComponent<Zombie>();
            if (zombie != null) {
                var zombieTransform = zombie.transform;
                var explosion = _zombieExplosion.Clone();
                explosion.transform.position = zombieTransform.position;
                _coinSrc.Clone().transform.position = zombieTransform.position + VectorUtils.RandomV3(-1f, 1f).ToVector3XZ();
                _expSrc.Clone().transform.position = zombieTransform.position + VectorUtils.RandomV3(-1f, 1f).ToVector3XZ();
            }
        }

        private IEnumerator C_Win() {
            var timeUntilWin = 3f;
            var timer = 0f;
            var deathShown = false;
            while (timer < timeUntilWin) {
                if (!deathShown && timer >= timeUntilWin / 4) {
                    player.ShowDeath();
                    deathShown = true;
                }
                Time.timeScale = Mathf.Lerp(Time.timeScale, 0.3f, Time.deltaTime);
                timer += Time.deltaTime;
                yield return null;
            }
            Time.timeScale = 1f;
            _outOfLoopFrame.Show();
        }

        private void OnDestroy() {
            Debug.LogError("Game controller destroyed");
        }

        #endregion
    }
}