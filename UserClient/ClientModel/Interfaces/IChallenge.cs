using System.Collections.Generic;

namespace ClientModels.Interfaces
{
    public interface IChallenge
    {
        long ChallengeID { get; set; }
        string InitialCode { get; set; }
        Language Language { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        List<ITest> Tests { get; set; }

        IChallenge GetCopy();
    }
}