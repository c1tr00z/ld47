using c1tr00z.AssistLib.GameUI;
using c1tr00z.ld47.Gameplay;

namespace Gameplay.UI.Code {
    public class PlayerZombieView : UIItemView<PlayerZombie> {
        
        public Life life => item.GetLife();
        
        public float expValue => currentExp * 1f / item.maxExp;

        public int currentExp => (item.exp % item.maxExp);

        public int maxXp => item.maxExp;

        public int level => item.level;

        public int coins => item.coins;

        public bool hasCoins => coins > 0;

        private void OnEnable() {
            PlayerZombie.changed += PlayerZombieOnchanged;
        }

        private void OnDisable() {
            PlayerZombie.changed -= PlayerZombieOnchanged;
        }

        private void PlayerZombieOnchanged() {
            OnDataChanged();
        }
    }
}