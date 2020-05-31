using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesProcessing.Models;

namespace MoviesProcessing.Services
{
   public interface IYouTubeService
   {
      Task<IEnumerable<YouTubeGetItem>> GetById(string query);
      Task<IEnumerable<YouTubeSearchItem>> Search(string query);
      Task<IEnumerable<YouTubeGetItem>> SearchStats(string query);
   }
}