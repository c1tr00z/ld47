using System;
using c1tr00z.AssistLib.GameUI;
using c1tr00z.ld47.Gameplay;

namespace Gameplay.UI.Code {
    public class PlayerUIView : UIItemView<Player> {

        public Life life => item.GetLife();

        public float expValue => currentExp * 1f / maxXp;

        public int currentExp => (item.exp % item.maxExp);

        public int maxXp => item.maxExp;

        public int level => item.level;

        private void OnEnable() {
            Player.changed += PlayerOnchanged;
        }
        
        private void OnDisable() {
            Player.changed -= PlayerOnchanged;
        }

        private void PlayerOnchanged() {
            OnDataChanged();
        }
    }
}