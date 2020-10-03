using System;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class PlayerZombie : Movable {
        
        

        [SerializeField] private KeyCode _upKey;
        
        [SerializeField] private KeyCode _downKey;
        
        [SerializeField] private KeyCode _rightKey;
        
        [SerializeField] private KeyCode _leftKey;
        
        private void Update() {
            
            var direction = new Vector3();
            
            if (Input.GetKey(_upKey)) {
                direction.z += 1;
            }
            if (Input.GetKey(_downKey)) {
                direction.z -= 1;
            }
            if (Input.GetKey(_rightKey)) {
                direction.x += 1;
            }
            if (Input.GetKey(_leftKey)) {
                direction.x -= 1;
            }

            MoveTo(direction);
        }
    }
}