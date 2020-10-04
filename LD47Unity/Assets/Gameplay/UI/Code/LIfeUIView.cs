using c1tr00z.AssistLib.GameUI;
using c1tr00z.ld47.Gameplay;

namespace Gameplay.UI.Code {
    public class LIfeUIView : UIItemView<Life> {

        public int maxLives => item.maxLives;

        public int currentLives => item.hp;

        public float lifeValue => item.lifeValue;

        private void OnEnable() {
            Life.damaged += LifeOnDamaged;
            Life.refreshed += LifeOnrefreshed;
        }

        private void OnDisable() {
            Life.damaged -= LifeOnDamaged;
            Life.refreshed -= LifeOnrefreshed;
        }

        private void LifeOnDamaged(Life life) {
            if (item != life) {
                return;
            }
            OnDataChanged();
        }

        private void LifeOnrefreshed(Life life) {
            if (item != life) {
                return;
            }
            OnDataChanged();
        }
    }
}