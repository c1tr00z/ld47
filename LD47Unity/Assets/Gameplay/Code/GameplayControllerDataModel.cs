using System;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.DataModels;

namespace c1tr00z.ld47.Gameplay {
    public class GameplayControllerDataModel : DataModelBase {

        #region Accessors

        public GameplayController gameplayController => Modules.Get<GameplayController>();

        public int secondsBeforePlayer => gameplayController.secondsBeforePlayer;

        public int justSeconds => secondsBeforePlayer % 60;

        public int minutes => secondsBeforePlayer / 60;

        public Player player => gameplayController.player;
        
        public PlayerZombie playerZombie => gameplayController.playerZombie;

        public bool playerIsHere => player != null;
        
        #endregion

        #region Unity Events

        private void OnEnable() {
            GameplayController.Changed += GameplayControllerOnChanged;
            GameplayController.playerSpawned += GameplayControllerOnplayerSpawned;
        }

        private void OnDisable() {
            GameplayController.Changed -= GameplayControllerOnChanged;
            GameplayController.playerSpawned -= GameplayControllerOnplayerSpawned;
        }

        #endregion

        #region Class Implementation

        private void GameplayControllerOnChanged() {
            OnDataChanged();
        }

        private void GameplayControllerOnplayerSpawned(Player obj) {
            OnDataChanged();
        }

        #endregion

    }
}