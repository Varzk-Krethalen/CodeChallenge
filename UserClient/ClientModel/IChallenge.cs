using System.Collections.Generic;

namespace ClientModels
{
    public interface IModel
    {
        Challenge CurrentChallenge { get; set; }
        List<Challenge> Challenges { get; set; }
        User CurrentUser { get; set; }
        List<Ranking> Rankings { get; set; }
    }
}
