using ClientModels.Interfaces;
using ClientModels.RemoteModelObjects;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ClientModels
{
    public class RemoteRanking : IRanking
    {
        [JsonProperty("rankingName")]
        public string RankingName { get; set; }

        [JsonProperty("ranks", ItemConverterType = (typeof(JsonConcreteConverter<IRank, RemoteRank>)))]
        public List<IRank> Ranks { get; set; }
    }
}