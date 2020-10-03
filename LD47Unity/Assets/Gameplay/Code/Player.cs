using UnityEngine;

namespace c1tr00z.ld47.Gameplay {
    public class Player: Movable {

        #region Private Fields

        private ZombieRadar _zombieRadar;

        #endregion

        #region Accessors

        public ZombieRadar zombieRadar => this.GetCachedComponentInChildren(ref _zombieRadar);

        #endregion

        #region Movable Implementation

        protected override void LateUpdate() {

            base.LateUpdate();

            var nearestZombie = zombieRadar.GetNearest();

            if (nearestZombie == null) {
                return;
            }

            var direction = transform.GetDirection(nearestZombie.GetTransform());
            
            Debug.LogError(direction);
            
            transform.rotation.SetLookRotation(direction);
        }

        #endregion

        #region Class Implementation

        

        #endregion
    }
}