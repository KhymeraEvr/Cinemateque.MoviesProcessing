using System.Collections.Generic;
using Newtonsoft.Json;

namespace MoviesProcessing.Models
{
   public class Movie
   {
      [JsonProperty("poster_path")]
      public string Poster { get; set; }

      [JsonProperty("overview")]
      public string Overview { get; set; }

      [JsonProperty("release_date")]
      public string ReleaseDate { get; set; }

      [JsonProperty("genre_ids")]
      public IEnumerable<int> GenreIds { get; set; }

      public IEnumerable<string> Genres { get; set; }

      [JsonProperty("id")]
      public int Id { get; set; }

      [JsonProperty("original_language")]
      public string OriginalLanguage { get; set; }

      [JsonProperty("original_title")]
      public string OriginalTitle { get; set; }

      [JsonProperty("title")]
      public string Title { get; set; }

      [JsonProperty("popularity")]
      public double Popularity { get; set; }

      [JsonProperty("vote_count")]
      public int VoteCount { get; set; }

      [JsonProperty("vote_average")]
      public double VoteAverage { get; set; }
   }
}
