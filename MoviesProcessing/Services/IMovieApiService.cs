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
      Task<IEnumerable<Movie>> GetActorMovies(string actorId);
      Task<IEnumerable<Movie>> GetCrewMovies(string crewId);
      Task<IEnumerable<Movie>> GetTopRatedMoves(int page);
      Task<MovieDetails> GetMovieDetails(string id);
      Task<PersonDetails> GetPersonDetails(string id);
   }
}