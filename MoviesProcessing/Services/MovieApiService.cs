using MoviesProcessing.Models;
using MoviesProcessing.Models.Responses;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesProcessing.Services
{
   public class MovieApiService : IMovieApiService
   {
      private readonly IRestClient _restClient;
      private readonly IConfiguration _config;

      private const string _discoverResource = "/discover/movie";
      private const string _genresResource = "/genre/movie/list";
      private const string _creditsResource = "/movie/movie_id/credits";
      private const string _apiKey = "MovieDbKey";
      private const string _apiKeyParameter = "api_key";
      private const string _movieIdParameter = "movie_id";

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

      private IRestRequest GetRequest(string resource)
      {
         var request = new RestRequest(resource, Method.GET);
         request.AddParameter(_apiKeyParameter, _config[_apiKey]);

         return request;
      }
   }
}
