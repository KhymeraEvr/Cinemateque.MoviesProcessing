using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesProcessing.Models.Responses
{
   public class CreditsResponse
   {
      [JsonProperty("id")]
      public int Id { get; set; }

      [JsonProperty("cast")]
      public IEnumerable<CastModel> Cast { get; set; }

      [JsonProperty("crew")]
      public IEnumerable<CrewModel> Crew { get; set; }
   }
}
