using System.Collections.Generic;
using System.Linq;
using c1tr00z.AssistLib.AppModules;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class CollidersController : Module {

        #region Nester Classes

        private class ColliderObjects {
            public Collider collider;
            public List<Object> objects = new List<Object>();
        }

        #endregion

        #region Private Fields

        private List<ColliderObjects> _collidersObjects = new List<ColliderObjects>();

        #endregion

        #region Class Implementation

        public void AddToColliders(Collider collider, Object obj) {
            var colliderObjects = _collidersObjects.FirstOrDefault(co => co.collider == collider);
            if (colliderObjects == null) {
                colliderObjects = new ColliderObjects {
                    collider = collider,
                };
            }

            if (!colliderObjects.objects.Contains(obj)) {
                colliderObjects.objects.Add(obj);
            }

            if (!_collidersObjects.Contains(colliderObjects)) {
                _collidersObjects.Add(colliderObjects);
            }
        }

        public void RemoveFromColliders(Collider collider) {
            var colliderObjects = _collidersObjects.FirstOrDefault(co => co.collider == collider);
            if (colliderObjects == null) {
                return;
            }

            _collidersObjects.Remove(colliderObjects);
        }

        public T GetColliderComponent<T>(Collider collider) {
            var colliderObjects = _collidersObjects.FirstOrDefault(co => co.collider == collider);
            if (colliderObjects == null) {
                return default;
            }

            return colliderObjects.objects.OfType<T>().FirstOrDefault();
        }

        #endregion
        
    }
}