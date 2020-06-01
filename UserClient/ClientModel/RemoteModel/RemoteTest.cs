using ClientModels.Interfaces;
using Newtonsoft.Json;

namespace ClientModels.RemoteModelObjects
{
    public class RemoteTest : ITest
    {
        [JsonProperty("testID")]
        public long TestID { get; set; }

        [JsonProperty("inputArgs")]
        public string InputArgs { get; set; } = string.Empty;

        [JsonProperty("expectedOutput")]
        public string ExpectedOutput { get; set; } = string.Empty;
    }
}
