using ClientModels.Interfaces;

namespace ClientModels.RemoteModelObjects
{
    public class RemoteTest : ITest
    {
        public long testID { get; set; }
        public string inputArgs { get; set; } = string.Empty;
        public string expectedOutput { get; set; } = string.Empty;
    }
}
