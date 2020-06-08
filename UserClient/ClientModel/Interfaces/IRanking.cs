using System.Collections.Generic;

namespace ClientModels.Interfaces
{
    public interface IRanking
    {
        string RankingName { get; }
        List<IRank> Ranks { get; }
    }
}
