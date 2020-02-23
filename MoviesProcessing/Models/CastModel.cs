using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesProcessing.Models
{
   public class CastModel
   {
      [JsonProperty("cast_id")]
      public int CastId { get; set; }

      [JsonProperty("character")]
      public string Character { get; set; }

      [JsonProperty("credit_id")]
      public string CreditId { get; set; }

      [JsonProperty("gender")]
      public int? Gender { get; set; }

      [JsonProperty("id")]
      public int Id { get; set; }

      [JsonProperty("name")]
      public string Name { get; set; }

      [JsonProperty("order")]
      public int Oreder { get; set; }

      [JsonProperty("profile_path")]
      public string ProfilePath { get; set; }
   }
}
