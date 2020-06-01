using ClientModels.Interfaces;
using Newtonsoft.Json;

namespace ClientModels.RemoteModelObjects
{
    public class RemoteTest : ITest
    {
        [JsonProperty(PropertyName = "testID")]
        public long TestID { get; set; }

        [JsonProperty(PropertyName = "inputArgs")]
        public string InputArgs { get; set; } = string.Empty;

        [JsonProperty(PropertyName = "expectedOutput")]
        public string ExpectedOutput { get; set; } = string.Empty;
    }
}
