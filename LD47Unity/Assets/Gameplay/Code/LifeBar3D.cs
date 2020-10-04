using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class LifeBar3D : MonoBehaviour {

        private Transform _transform;

        public Transform transform => this.GetCachedComponent(ref _transform);

        public float maxWidth = 1;
        
        public SpriteRenderer spriteRenderer;

        public void SetValue(float value) {
            var size = transform.localScale;
            size.x = maxWidth * value;
            transform.localScale = size;
        }
    }
}