using Newtonsoft.Json;

namespace MoviesProcessing.Models
{
   public class PersonDetails
   {
      [JsonProperty("id")]
      public int Id { get; set; }

      [JsonProperty("name")]
      public string Name { get; set; }

      [JsonProperty("known_for_department")]
      public string Department { get; set; }

      [JsonProperty("popularity")]
      public double Popularity { get; set; }
   }
}
