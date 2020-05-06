using ClientModels;
using System.Collections.Generic;

namespace ClientModel
{
    class RemoteModel : IModel
    {
        public Challenge CurrentChallenge { get; set; }
        public List<Challenge> Challenges { get; set; }
        public User CurrentUser { get; set; }
        public List<Ranking> Rankings { get; set; }
    }
}
