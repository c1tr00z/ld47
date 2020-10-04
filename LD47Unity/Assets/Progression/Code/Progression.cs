using System;
using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.SceneManagement;
using UnityEngine;

namespace c1tr00z.ld47.Progression {
    public class Progression : Module {

        public static event Action changed;

        private ProgressionSave _save;

        public ProgressionSave save {
            get {
                if (_save == null) {
                    Load();
                }

                return _save;
            }
        }

        public void SetCoins(int value) {
            _save.coins = value;
            Save();
            changed?.Invoke();
        }

        public void AddCoin() {
            SetCoins(save.coins + 1);
        }

        public void RemoveAllCoins() {
            SetCoins(0);
        }

        private void OnEnable() {
            Load();
            Scenes.sceneLoaded += ScenesOnsceneLoaded;
        }

        private void OnDisable() {
            Scenes.sceneLoaded -= ScenesOnsceneLoaded;
        }

        private void Save() {
            var jsonString = save.ToJsonString();
            PlayerPrefs.SetString(GetType().FullName, jsonString);
        }

        private void Load() {
            var jsonString = PlayerPrefs.GetString(GetType().FullName);
            if (string.IsNullOrEmpty(jsonString)) {
                _save = new ProgressionSave();
                return;
            }

            _save = JSONUtuls.FromJsonString<ProgressionSave>(jsonString);
        }

        private void ScenesOnsceneLoaded(SceneItem obj) {
            PlayerPrefs.Save();
        }
        
    }
}