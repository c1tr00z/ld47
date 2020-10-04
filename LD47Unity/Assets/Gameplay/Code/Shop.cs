using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace c1tr00z.ld47.Gameplay {
    public class Shop : MonoBehaviour {

        public bool isActive { get; private set; } = true;

        public int price;

        public GameObject visual;

        public TextMeshPro text;

        public AudioSource sound;

        private void Start() {
            text.text = price.ToString();
        }

        private void OnTriggerEnter(Collider other) {

            if (!isActive) {
                return;
            }
            
            var playerZombie = other.GetColliderComponent<PlayerZombie>();

            if (playerZombie == null) {
                return;
            }

            if (playerZombie.GetCoins(price)) {
                sound.transform.parent = null;
                sound.Play();
                isActive = false;
                visual.SetActive(false);
                playerZombie.AddShotgun();
            }
        }
    }
}