using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace c1tr00z.ld47.Gameplay {
    public class SelfDestructParticles : MonoBehaviour {

        public bool playOnStart;
        public ParticleSystem particles;

        private bool started = false;

        private void Start() {
            if (playOnStart) {
                Play();
            }
        }

        public void Play() {
            started = true;
            particles.Play();
        }

        private void Update() {
            if (!started) {
                return;
            }

            if (particles.isPlaying) {
                return;
            }
            
            Destroy(gameObject);
        }


    }
}