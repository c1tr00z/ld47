using System;
using c1tr00z.AssistLib.ResourcesManagement;
using c1tr00z.ld47.Gameplay;
using UnityEngine;

namespace c1tr00z.ld47.MainMenu {
    public class MenuSceneController : MonoBehaviour {

        public int count;
        
        public ZombiesSpawner spawner;

        private void Start() {
            spawner.ClearSpawnData();
            new [] { count }.Iterate(i => {
                var zombie = DB.Get<ZombieDBEntry>().LoadPrefab<Zombie>().Clone();
                spawner.SpawnAtRandom(zombie.transform);
            });
        }
    }
}