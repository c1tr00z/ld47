using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace c1tr00z.ld47.Gameplay {
    public class PIckableCollector : MonoBehaviour {
        
        public List<AudioSource> coinsAudioSources = new List<AudioSource>();
        
        public List<AudioSource> expAudioSources = new List<AudioSource>();

        public UnityEvent coinCollected;

        public UnityEvent expCollected;

        public bool playSound;

        private void OnTriggerEnter(Collider other) {
            var coin = other.GetColliderComponent<Coin>();
            if (coin != null) {
                if (playSound) {
                    coinsAudioSources.FirstOrDefault(cas => !cas.isPlaying)?.Play();
                }
                Destroy(coin.gameObject);
                coinCollected.Invoke();
                return;
            }
            
            var exp = other.GetColliderComponent<Exp>();
            if (exp != null) {
                if (playSound) {
                    expAudioSources.FirstOrDefault(eas => !eas.isPlaying)?.Play();
                }
                Destroy(exp.gameObject);
                expCollected.Invoke();
                return;
            }
        }
    }
}