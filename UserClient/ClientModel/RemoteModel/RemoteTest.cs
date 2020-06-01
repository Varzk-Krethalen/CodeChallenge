using ClientModels.Interfaces;

namespace ClientModels.RemoteModelObjects
{
    public class RemoteTest : ITest
    {
        public long testID { get; set; }
        public string inputArgs { get; set; }
        public string expectedOutput { get; set; }
    }
}
