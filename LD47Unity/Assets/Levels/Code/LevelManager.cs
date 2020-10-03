using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.SceneManagement;

namespace c1tr00z.ld47.Levels {
    public class LevelManager : Module {

        #region Accessors

        public Scenes scenes => Modules.Get<Scenes>();

        public LevelDBEntry currentLevel => scenes.currentSceneItem as LevelDBEntry;

        #endregion

        #region Unity Events

        private void OnEnable() {
            Scenes.sceneLoaded += ScenesOnSceneLoaded;
        }

        private void OnDisable() {
            Scenes.sceneLoaded -= ScenesOnSceneLoaded;
        }

        #endregion

        #region Class Implementation

        private void ScenesOnSceneLoaded(SceneItem sceneItem) {
            
        }

        public void LoadLevel(LevelDBEntry levelDBEntry) {
            scenes.LoadScene(levelDBEntry);
        }

        #endregion
    }
}