using c1tr00z.AssistLib.AppModules;
using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public static class CollidersUtils {

        public static bool GetController(out CollidersController controller) {
            controller = Modules.Get<CollidersController>();
            return controller != null;
        }

        public static void AddToColliders(this Collider collider, Object obj) {
            if (GetController(out CollidersController controller)) {
                controller.AddToColliders(collider, obj);
            }
        }
        
        public static void RemoveFromColliders(this Collider collider) {
            if (GetController(out CollidersController controller)) {
                controller.RemoveFromColliders(collider);
            }
        }

        public static T GetColliderComponent<T>(this Collider collider) {
            if (!GetController(out CollidersController controller)) {
                return default;
            }
            return controller.GetColliderComponent<T>(collider);
        }
    }
}