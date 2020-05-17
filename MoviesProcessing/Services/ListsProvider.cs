using System.Collections.Generic;

namespace MoviesProcessing.Services
{
   public static class ListsProvider
   {
      public readonly static List<string> Companies = new List<string>
      {
         "20th Century Fox",
         "A24",
         "Castle Rock Entertainment",
         "Columbia Pictures",
         "DC Comics",
         "DC Entertainment",
         "DreamWorks Animation",
         "DreamWorks Pictures",
         "Dovzhenko Film Studios",
         "DENTSU Music And Entertainment",
         "Fox 2000 Pictures",
         "Fox Searchlight Pictures",
         "Fine Line Features",
         "Globo Filmes",
         "Legendary Entertainment",
         "Lucasfilm",
         "Lionsgate Premiere",
         "Marvel Studios",
         "Miramax",
         "Metro-Goldwyn-Mayer",
         "New Line Cinema",
         "New Regency Pictures",
         "Nippon Television Network Corporation",
         "Paramount",
         "Pixar",
         "Roadside Attractions",
         "Shochiku Co., Ltd.",
         "Studio Ghibli",
         "StudioCanal",
         "Syncopy",
         "Sony Pictures",
         "Sony Pictures Imageworks",
         "Sony Pictures Animation",
         "Sony Pictures Classics",
         "The Saul Zaentz Company",
         "Toho Company, Ltd.",
         "Tokuma Shoten",
         "The Weinstein Company",
         "United Artists",
         "Universal Pictures",
         "Universal Studios Home Entertainment",
         "Walt Disney Pictures",
         "Warner Bros.Animation",
         "Warner Bros.Pictures",
         "Warner Bros. Entertainment",
         "WingNut Films",
         "Focus Features",
      };

      public readonly static List<string> Genres = new List<string>
      {
           "Action",
           "Adventure",
           "Animation",
           "Comedy",
           "Crime",
           "Documentary",
           "Drama",
           "Family",
           "Fantasy",
           "History",
           "Horror",
           "Music",
           "Mystery",
           "Romance",
           "Science Fiction",
           "TV Movie",
           "Thriller",
           "War",
           "Western"
      };

      public readonly static HashSet<string> Jobs = new HashSet<string>
      {
         "director", "screenplay", "story", "original music composer"
      };
   }
}
