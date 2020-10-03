using System;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.AssistLib.SceneManagement;
using UnityEngine;

namespace c1tr00z.ld47.MainMenu {
    public class MenuController : Module {

        #region Private Fields

        private Scenes _scenes;

        #endregion
        
        #region Serialized Fields

        [SerializeField] private SceneItem _menuScene;
        
        [SerializeField] private SceneItem _gameScene;

        [SerializeField] private UIFrameDBEntry _menuFrame;

        #endregion

        #region Serialize Fields

        public Scenes scenes => ModulesUtils.GetCachedModule(ref _scenes);

        #endregion

        #region Unity Events

        private void Start() {
            ShowMenuFrame();
        }

        #endregion

        #region Class Implementation

        public void Play() {
            _gameScene.Load(true);
        }

        public void GoToMenu() {
            _menuScene.LoadAsync(ShowMenuFrame);
        }

        private void ShowMenuFrame() {
            if (scenes.currentSceneItem != _menuScene) {
                return;
            }
            _menuFrame.Show();
        }

        #endregion

    }
}