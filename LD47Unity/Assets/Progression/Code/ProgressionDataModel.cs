using c1tr00z.AssistLib.AppModules;
using c1tr00z.AssistLib.DataModels;

namespace c1tr00z.ld47.Progression {
    public class ProgressionDataModel : DataModelBase {

        private Progression _progression;

        public Progression progression => ModulesUtils.GetCachedModule(ref _progression);

        public bool hasSave => progression.save.coins > 0;

        public void RemoveAllCoins() {
            progression.RemoveAllCoins();
        }
    }
}