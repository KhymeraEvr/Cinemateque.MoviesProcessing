using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesProcessing.Models;
using MoviesProcessing.Models.Responses;

namespace MoviesProcessing.Services
{
   public interface IMovieApiService
   {
      Task<IEnumerable<Movie>> GetDiscoverFilms();
      Task<IEnumerable<Genre>> GetGenres();
      Task<CreditsResponse> GetCredits(string id);
   }
}