using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesProcessing.Models
{
   public class GenresResponseModel
   {
      [JsonProperty("genres")]
      public IEnumerable<Genre> Genres { get; set; }
   }
}
