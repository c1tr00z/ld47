using System;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class Pickable : MonoBehaviour {

        private Collider _collider;

        public GameObject visual;

        private bool _started;

        public Collider collider => this.GetCachedComponent(ref _collider);
        
        private void Awake() {
            collider.AddToColliders(this);
        }

        private void OnDestroy() {
            collider.RemoveFromColliders();
        }
    }
}