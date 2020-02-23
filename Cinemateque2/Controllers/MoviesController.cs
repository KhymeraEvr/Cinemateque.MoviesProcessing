using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesProcessing.Services;

namespace Cinemateque2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
      private readonly IMovieApiService _movieService;
      private readonly ICachedGenresService _genresService;

      public MoviesController(IMovieApiService movieService, ICachedGenresService genresService)
      {
         _movieService = movieService;
         _genresService = genresService;
      }

      [HttpGet("discover")]
      public async Task<IActionResult> Discover()
      {
         var movies = await _movieService.GetDiscoverFilms();
         foreach( var movie in movies )
         {
            var genresList = await Task.WhenAll(movie.GenreIds.Select( async gid => await _genresService.GetGenreById(gid)));
            movie.Genres = genresList;
         }

         return Ok(movies);
      }

      [HttpGet("{movieId}/credits")]
      public async Task<IActionResult> Credits( [FromRoute] string movieId )
      {
         var credits = await _movieService.GetCredits(movieId);

         return Ok(credits);
      }
   }
}