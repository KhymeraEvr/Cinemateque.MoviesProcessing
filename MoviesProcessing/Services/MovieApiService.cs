using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MoviesProcessing.Models;
using MoviesProcessing.Models.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace MoviesProcessing.Services
{
   public class MovieApiService : IMovieApiService
   {
      private readonly IRestClient _restClient;
      private readonly IConfiguration _config;

      private const string _discoverResource = "/3/discover/movie";
      private const string _genresResource = "3/genre/movie/list";
      private const string _creditsResource = "/3/movie/movie_id/credits";
      private const string _topRatedResource = "/3/movie/top_rated";
      private const string _detailsResource = "3/movie/movie_id";
      private const string _personResource = "3/person/person_id";

      private const string _apiKey = "MovieDbKey";

      private const string _apiKeyParameter = "api_key";
      private const string _movieIdParameter = "movie_id";
      private const string _withCastParameter = "with_cast";
      private const string _withCrewParameter = "with_crew";
      private const string _languageParameter = "language";
      private const string _pageParameter = "page";
      private const string _personIdParameter = "person_id";

      public MovieApiService(IConfiguration config)
      {
         _config = config;
         _restClient = new RestClient(_config["MovieDbBaseUrl"]);
      }

      public async Task<IEnumerable<Movie>> GetDiscoverFilms()
      {
         var request = GetRequest(_discoverResource);

         var response = await _restClient.ExecuteGetAsync(request);
         var movies = JsonConvert.DeserializeObject<MoviesResponseModel>(response.Content);

         return movies.Movies;
      }

      public async Task<IEnumerable<Genre>> GetGenres()
      {
         var request = GetRequest(_genresResource);

         var response = await _restClient.ExecuteGetAsync(request);
         var genres = JsonConvert.DeserializeObject<GenresResponseModel>(response.Content);

         return genres.Genres;
      }

      public async Task<CreditsResponse> GetCredits(string id)
      {
         var resource = _creditsResource.Replace(_movieIdParameter, id);
         var request = GetRequest(resource);

         var response = await _restClient.ExecuteGetAsync(request);
         var credits = JsonConvert.DeserializeObject<CreditsResponse>(response.Content);

         return credits;
      }

      public async Task<IEnumerable<Movie>> GetActorMovies(string actorId)
      {
         var request = GetRequest(_discoverResource);

         request.AddParameter(_withCastParameter, actorId);
         var movies = await GetAllMoviePages(request);

         return movies;
      }

      public async Task<IEnumerable<Movie>> GetCrewMovies(string crewId)
      {
         var request = GetRequest(_discoverResource);

         request.AddParameter(_withCrewParameter, crewId);
         var movies = await GetAllMoviePages(request);

         return movies;
      }

      public async Task<IEnumerable<Movie>> GetTopRatedMoves(int page)
      {
         var request = GetRequest(_topRatedResource);
         request.AddParameter(_languageParameter, "en-US");
         request.AddParameter(_pageParameter, page);
         var response = await _restClient.ExecuteGetAsync(request);

         var movies = JsonConvert.DeserializeObject<MoviesResponseModel>(response.Content);
         var filtered = FilterMovies(movies.Movies);

         return filtered;
      }

      public async Task<MovieDetails> GetMovieDetails(string id)
      {
         var resource = _detailsResource.Replace(_movieIdParameter, id);
         var request = GetRequest(resource);

         request.AddParameter(_languageParameter, "en-US");
         var response = await _restClient.ExecuteGetAsync(request);

         var movie = JsonConvert.DeserializeObject<MovieDetails>(response.Content);

         return movie;
      }
      
      public async Task<PersonDetails> GetPersonDetails(string id)
      {
         var resource = _personResource.Replace(_personIdParameter, id);
         var request = GetRequest(resource);

         var response = await _restClient.ExecuteGetAsync(request);

         var details = JsonConvert.DeserializeObject<PersonDetails>(response.Content);

         return details;
      }

      private IEnumerable<Movie> FilterMovies(IEnumerable<Movie> movies)
      {
         var filtered = movies.Where(movie => movie.OriginalLanguage != "hi");

         return filtered;
      }

      private async Task<IEnumerable<Movie>> GetAllMoviePages(IRestRequest request)
      {
         var items = new List<Movie>();
         MoviesResponseModel responseModel = null;

         do
         {
            var response = await _restClient.ExecuteGetAsync(request);
            responseModel = JsonConvert.DeserializeObject<MoviesResponseModel>(response.Content);
            items.AddRange(responseModel.Movies);
            var currentPage = responseModel.Page;
            request.AddParameter(_pageParameter, ++currentPage);


         } while (responseModel.Page != responseModel.TotalPages);

         return items;
      }

      private IRestRequest GetRequest(string resource)
      {
         var request = new RestRequest(resource, Method.GET);
         request.AddParameter(_apiKeyParameter, _config[_apiKey]);

         return request;
      }
   }
}
