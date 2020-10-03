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
        
        #endregion

        #region Unity Events

        private void OnEnable() {
            GameplayController.Changed += GameplayControllerOnChanged;
        }

        private void OnDisable() {
            GameplayController.Changed -= GameplayControllerOnChanged;
        }

        #endregion

        #region Class Implementation

        private void GameplayControllerOnChanged() {
            OnDataChanged();
        }

        #endregion

    }
}