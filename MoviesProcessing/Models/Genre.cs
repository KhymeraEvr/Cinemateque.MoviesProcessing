﻿using Newtonsoft.Json;

namespace MoviesProcessing.Models
{
   public class Genre
   {
      [JsonProperty("name")]
      public string Name { get; set; }

      [JsonProperty("id")]
      public int Id { get; set; }
   }
}
