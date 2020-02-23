using System.Threading.Tasks;

namespace MoviesProcessing.Services
{
   public interface ICachedGenresService
   {
      Task<string> GetGenreById(int id);
      Task RefreshCache();
   }
}