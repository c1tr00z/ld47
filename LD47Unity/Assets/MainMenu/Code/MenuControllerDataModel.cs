using System;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.DataModels;

namespace c1tr00z.ld47.MainMenu {
    public class MenuControllerDataModel : DataModelBase {

        #region Accessors

        public MenuController menuController => Modules.Get<MenuController>();

        #endregion

        #region Class Implementation

        public void Play() {
            menuController.Play();
        }

        public void GoToMenu() {
            menuController.GoToMenu();
        }

        #endregion
    }
}