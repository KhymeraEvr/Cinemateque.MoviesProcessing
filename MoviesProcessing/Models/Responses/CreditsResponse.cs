using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
