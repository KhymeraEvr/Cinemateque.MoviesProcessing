using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace MoviesProcessing.Services
{
   public class CachedGenresService : ICachedGenresService
   {
      private readonly IMemoryCache _cache;
      private readonly IMovieApiService _movieService;

      public CachedGenresService(IMemoryCache memoryCache, IMovieApiService movieService)
      {
         _cache = memoryCache;
         _movieService = movieService;
      }

      public async Task RefreshCache()
      {
         var genres = await _movieService.GetGenres();

         foreach (var genre in genres)
         {
            _cache.Set(genre.Id, genre.Name);
         }
      }

      public async Task<string> GetGenreById(int id)
      {
         string genre = null;
         if (_cache.TryGetValue(id, out genre))
         {
            return genre;
         }
         else
         {
            await RefreshCache();
            if (_cache.TryGetValue(id, out genre))
            {
               return genre;
            }
            else
            {
               return "Unknown genre";
            }
         }
      }
   }
}
