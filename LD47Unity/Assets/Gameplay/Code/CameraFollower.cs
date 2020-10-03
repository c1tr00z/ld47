using System;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class CameraFollower : MonoBehaviour {

        #region Serialized Fields

        [SerializeField] private Transform _target;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _speed;

        #endregion

        #region Unity Events

        private void OnEnable() {
            GameplayController.playerZombieSpawned += GameplayControllerOnplayerZombieSpawned;
        }

        private void LateUpdate() {

            if (_target == null) {
                return;
            }
            
            transform.position = Vector3.Lerp(transform.position, _target.position + _offset,
                Time.deltaTime * _speed);
        }

        #endregion
        
        private void GameplayControllerOnplayerZombieSpawned(PlayerZombie playerZombie) {
            _target = playerZombie.transform;
        }
    }
}