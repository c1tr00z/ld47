using System;
using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.Sprites;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class MousePositionPrefixSetter : SpriteAnimatorPrefixSetter {
        #region Nested Classes

        private enum Directions {
            Left,
            Right,
            Up,
            Down
        }

        [Serializable]
        private class DirectionPrefix {
            public Directions direction;
            public string prefix;
        }

        #endregion

        #region Private Fields

        private Directions _lastDirection = Directions.Right;

        private Rigidbody _rigidbody;

        #endregion
        
        #region Serialized Fields

        [SerializeField] private List<DirectionPrefix> _directions = new List<DirectionPrefix>();
        
        #endregion

        #region Accessors

        public Rigidbody rigidbody => this.GetCachedComponent(ref _rigidbody);
        
        public Vector3 heading { get; private set; }
        
        public float mouseDistance { get; private set; }
        
        public Vector3 lookingDirection { get; private set; }

        #endregion
        
        #region Unity Events

        private void Start() {
            RefreshPrefix(_lastDirection);
        }

        private void LateUpdate() {
            SetPrefix();
        }

        #endregion

        #region Class Implemenetation

        private void SetPrefix() {

            RefreshMouseInfo();

            var direction = _lastDirection;
            
            if (Mathf.Abs(lookingDirection.x) > Mathf.Abs(lookingDirection.z)) {
                direction = lookingDirection.x > 0 ? Directions.Right : Directions.Left;
            } else {
                direction = lookingDirection.z > 0 ? Directions.Up : Directions.Down;
            }

            _lastDirection = direction;

            RefreshPrefix(direction);
        }

        private void RefreshPrefix(Directions direction) {
            currentPrefix = _directions.FirstOrDefault(d => d.direction == direction)?.prefix;
            _lastDirection = direction;
        }

        private void RefreshMouseInfo() {

            var screenWidth = Screen.width;
            var screenHeight = Screen.height;

            var screenCenterX = screenWidth / 2;
            var screenCenterY = screenHeight / 2;
            
            var center = new Vector3(screenCenterX, 0, screenCenterY);

            var mousePosition = Input.mousePosition.ToVector3XZ();

            heading = mousePosition - center;
            
            mouseDistance = heading.magnitude;
            
            lookingDirection = (heading / mouseDistance).RemoveY(); // This is now the normalized direction.
        }

        #endregion
    }
}