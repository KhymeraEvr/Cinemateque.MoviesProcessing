﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesProcessing.Models
{
   public class Genre
   {
      [JsonProperty( "name" )]
      public string Name { get; set; }

      [JsonProperty( "id" )]
      public int Id { get; set; }
   }
}