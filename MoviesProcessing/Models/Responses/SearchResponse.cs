using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace MoviesProcessing.Models.Responses
{
   public class SearchPersResponse
   {
      [JsonProperty("results")]
      public IEnumerable<CrewModel> Results { get; set; }
   }
}
