using System;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class CameraFollower : MonoBehaviour {
        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _speed;

        private void LateUpdate() {
            transform.position = Vector3.Lerp(transform.position, _target.position + _offset,
                Time.deltaTime * _speed);
        }
    }
}