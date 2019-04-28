using FwData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FwInMemDb
{
    public class MoviesDB
    {
        internal static List<Movie> Movies;
        static MoviesDB()
        {
            Initialize();
        }

        public static bool IsValidUser(int userId)
        {
            return userId > 0 && userId < 10;
        }

        private static void Initialize()
        {
            // 8 movies, 10 Users
            Movies = new List<Movie>(5)
            {
                new Movie { Id = 1, Title = "Abra Ka Dabra", RunningTime = 108, YearOfRelease = 1982, Genres = new List<Genre> { Genre.Drama, Genre.Mystery } },
                new Movie { Id = 2, Title = "My Nation", RunningTime = 208, YearOfRelease = 1992, Genres = new List<Genre> { Genre.Drama, Genre.Documentry } },
                new Movie { Id = 3, Title = "Marvels", RunningTime = 128, YearOfRelease = 2012, Genres = new List<Genre> { Genre.Kids, Genre.SciFi, Genre.Mystery } },
                new Movie { Id = 4, Title = "Last Resort", RunningTime = 128, YearOfRelease = 1956, Genres = new List<Genre> { Genre.Horror, Genre.Mystery } },
                new Movie { Id = 5, Title = "Last Code", RunningTime = 111, YearOfRelease = 2011, Genres = new List<Genre> { Genre.Mystery } },
                new Movie { Id = 6, Title = "Last Z", RunningTime = 111, YearOfRelease = 2011, Genres = new List<Genre> { Genre.Mystery } },
                new Movie { Id = 7, Title = "Last A", RunningTime = 111, YearOfRelease = 2011, Genres = new List<Genre> { Genre.Mystery } },
                new Movie { Id = 8, Title = "Last D", RunningTime = 111, YearOfRelease = 2011, Genres = new List<Genre> { Genre.Mystery } }
            };

            Movies[0].AddRating(1, 5);
            Movies[0].AddRating(5, 2);
            Movies[0].AddRating(7, 1);
            Movies[0].AddRating(8, 3);
            Movies[0].AddRating(9, 5);

            Movies[1].AddRating(2, 5);
            Movies[1].AddRating(5, 3);
            Movies[1].AddRating(1, 2);
            Movies[1].AddRating(8, 3);
            Movies[1].AddRating(9, 4);

            Movies[2].AddRating(2, 5);
            Movies[2].AddRating(3, 5);
            Movies[2].AddRating(1, 1);
            Movies[2].AddRating(8, 2);
            Movies[2].AddRating(9, 4);

            Movies[3].AddRating(1, 5);
            Movies[3].AddRating(2, 1);
            Movies[3].AddRating(3, 1);
            Movies[3].AddRating(4, 3);
            Movies[3].AddRating(5, 5);
            Movies[3].AddRating(6, 5);
            Movies[3].AddRating(7, 1);
            Movies[3].AddRating(8, 1);
            Movies[3].AddRating(9, 1);

            Movies[4].AddRating(2, 5);
            Movies[4].AddRating(5, 5);
            Movies[4].AddRating(1, 2);
            Movies[4].AddRating(7, 2);
            Movies[4].AddRating(9, 5);

            Movies[5].AddRating(2, 4);
            Movies[5].AddRating(5, 5);
            Movies[5].AddRating(7, 2);
            Movies[5].AddRating(1, 3);
            Movies[5].AddRating(3, 5);

            Movies[6].AddRating(2, 4);
            Movies[6].AddRating(3, 5);
            Movies[6].AddRating(4, 2);
            Movies[6].AddRating(5, 3);
            Movies[6].AddRating(1, 5);

            Movies[7].AddRating(1, 5);
            Movies[7].AddRating(2, 4);
            Movies[7].AddRating(5, 2);
            Movies[7].AddRating(6, 3);
            Movies[7].AddRating(7, 5);
        }
    }
}
