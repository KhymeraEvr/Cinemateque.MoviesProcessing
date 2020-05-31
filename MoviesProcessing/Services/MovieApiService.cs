using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Cinemateque.Models;
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
      private const string _searchResuource = "3/search/movie";
      private const string _searchPersResuource = "3/search/person";
      private const string _upcomingResource = "3/movie/upcoming";
      
      private const string _apiKey = "MovieDbKey";

      private const string _apiKeyParameter = "api_key";
      private const string _movieIdParameter = "movie_id";
      private const string _withCastParameter = "with_cast";
      private const string _withCrewParameter = "with_crew";
      private const string _withGenresParameter = "with_genres";
      private const string _languageParameter = "language";
      private const string _pageParameter = "page";
      private const string _personIdParameter = "person_id";
      private const string _queryParameter = "query";
      private const string _releaseDateParameter = "release_date.gte";

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

      public async Task<IEnumerable<Movie>> GetDiscoverFilms(int page)
      {
         var request = GetRequest(_discoverResource);
         request.AddParameter(_pageParameter, page);

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

      public async Task<IEnumerable<Movie>> SearchByModel(SearchModel model)
      {
         var request = GetRequest(_discoverResource);

         if (!string.IsNullOrEmpty(model.Director))
         {
            var ids = new List<string>();

            var perss = model.Director.Split(" ");
            foreach (var wha in perss)
            {
               var pers = await SearchPers(wha);

               ids.Add(pers.FirstOrDefault()?.Id.ToString());
            }

            request.AddParameter(_withCrewParameter, string.Join(',', ids));
         }

         if (!string.IsNullOrEmpty(model.Actor))
         {
            var ids = new List<string>();

            var perss = model.Actor.Split(" ");
            foreach (var wha in perss)
            {
               var pers = await SearchPers(wha);

               ids.Add(pers.FirstOrDefault()?.Id.ToString());
            }

            request.AddParameter(_withCastParameter, string.Join(',', ids ));
         }

         if (!string.IsNullOrEmpty(model.Genre))
         {
            var genres = model.Genre.Split(" ");
            var genresList = await GetGenres();
            var what = string.Join(',', genresList.Where(x => genres.Any(z => z.ToLower().Contains(x.Name.ToLower()))).Select(y => y.Id));

            request.AddParameter(_withGenresParameter, what);
         }

         if( model.Date.HasValue )
         {
            var format = model.Date.Value.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);

            request.AddParameter(_releaseDateParameter, format);

         }

         request.AddParameter(_pageParameter, 1);
         var response = await _restClient.ExecuteGetAsync(request);
         var responseModel = JsonConvert.DeserializeObject<MoviesResponseModel>(response.Content);

         return responseModel.Movies;
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

      public async Task<IEnumerable<Movie>> Search(string input)
      {
         var query = string.Join("+", input.Split(" "));
         var resource = _searchResuource;
         var request = GetRequest(resource);
         request.AddParameter(_queryParameter, query);

         var response = await _restClient.ExecuteGetAsync(request);
         var movies = JsonConvert.DeserializeObject<MoviesResponseModel>(response.Content);
         var filtered = FilterMovies(movies.Movies);

         return filtered;
      }

      public async Task<IEnumerable<CrewModel>> SearchPers(string input)
      {
         var query = string.Join("+", input.Split(" "));
         var resource = _searchPersResuource;
         var request = GetRequest(resource);
         request.AddParameter(_queryParameter, query);

         var response = await _restClient.ExecuteGetAsync(request);
         var movies = JsonConvert.DeserializeObject<SearchPersResponse>(response.Content);

         return movies.Results;
      }

      public async Task<IEnumerable<Movie>> GetUpdcoming(int page)
      {
         var resource = _upcomingResource;
         var request = GetRequest(resource);
         request.AddParameter(_pageParameter, page);

         var response = await _restClient.ExecuteGetAsync(request);
         var movies = JsonConvert.DeserializeObject<MoviesResponseModel>(response.Content);
         var filtered = FilterMovies(movies.Movies);

         return filtered;
      }

      public async Task AddCreditsToMoves(IEnumerable<Movie> movies)
      {
         foreach (var movie in movies)
         {
            var credits = await GetCredits(movie.Id.ToString());
            movie.Cast = credits.Cast;
            movie.Director = credits.Crew.FirstOrDefault(x => x.Job.ToLower() == "director");
         }
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
