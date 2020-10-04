using c1tr00z.AssistLib.Json;

namespace c1tr00z.ld47.Progression {
    public class ProgressionSave : IJsonSerializable, IJsonDeserializable {

        [JsonSerializableField]
        public int coins;

    }
}