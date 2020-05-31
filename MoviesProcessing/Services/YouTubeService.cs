using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MoviesProcessing.Models;
using MoviesProcessing.Models.Responses;
using Newtonsoft.Json;
using RestSharp;

namespace MoviesProcessing.Services
{
   public class YouTubeService : IYouTubeService
   {
      private readonly IRestClient _restClient;
      private readonly IConfiguration _config;

      private const string _apiKeyParameter = "key";
      private const string _apiKey = "YouTubeKey";
      private const string _partParameterKey = "part";
      private const string _snippet = "snippet";
      private const string _statistics = "statistics";
      private const string _maxResultsParameter = "maxResults";
      private const int _maxResults = 2;
      private const string _queryParameter = "q";
      private const string _idParameter = "id";

      private const string _searchResource = "/search";
      private const string _videosResource = "/videos";
      
      public YouTubeService(IConfiguration config)
      {
         _config = config;
         _restClient = new RestClient(_config["YouTubeApiBase"]);
      }

      public async Task<IEnumerable<YouTubeGetItem>> SearchStats(string query)
      {
         var searchRes = await Search(query);

         var result = new List<YouTubeGetItem>();

         foreach (var item in searchRes)
         {
            var getRes = await GetById(item.Id.Id);
            result.AddRange(getRes);
         }

         return result;
      }

      public async Task<IEnumerable<YouTubeSearchItem>> Search(string query)
      {
         query = query + " official trailer";
         var request = GetRequest(_searchResource);
         request.AddParameter(_queryParameter, query);
         request.AddParameter(_maxResultsParameter, _maxResults);

         var response = await _restClient.ExecuteGetAsync(request);
         var result = JsonConvert.DeserializeObject<YouTubeSearchResponse>(response.Content);

         return result.Items;
      }

      public async Task<IEnumerable<YouTubeGetItem>> GetById(string id)
      {
         var request = GetRequest(_videosResource);
         request.AddParameter(_idParameter, id);
         request.AddParameter(_partParameterKey, _statistics);

         var response = await _restClient.ExecuteGetAsync(request);
         var result = JsonConvert.DeserializeObject<YouTubeGetResponse>(response.Content);

         return result.Items;
      }

      private IRestRequest GetRequest(string resource)
      {
         var request = new RestRequest(resource, Method.GET);
         request.AddParameter(_apiKeyParameter, _config[_apiKey]);

         return request;
      }
   }
}
