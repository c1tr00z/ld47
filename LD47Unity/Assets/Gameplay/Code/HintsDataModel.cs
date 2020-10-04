using c1tr00z.AssistLib.DataModels;
using c1tr00z.AssistLib.ResourcesManagement;

namespace c1tr00z.ld47.Gameplay {
    public class HintsDataModel : DataModelBase {
        public Hints hints => DB.Get<Hints>();

        public string randomHint => hints.hints.RandomItem();
    }
}