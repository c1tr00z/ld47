using System;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.ResourcesManagement;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class GameplayController : Module {

        #region Events

        public static event Action<PlayerZombie> playerZombieSpawned;

        public static event Action Changed;

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

        #endregion

        #region Accessors

        public PlayerZombie playerZombie { get; private set; }
        
        public Player player { get; private set; }
        
        public int secondsBeforePlayer { get; private set; }

        #endregion

        #region Unity Events

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
        }

        #endregion
    }
}