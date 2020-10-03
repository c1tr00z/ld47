using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class Room : MonoBehaviour {
        
        [SerializeField] private ZombiesSpawner _spawner;
        
        [SerializeField] private Transform _playerSpawnPoint;

        public ZombiesSpawner spawner => _spawner;

        public Vector3 playerSpawnPoint => _playerSpawnPoint.position;
    }
}