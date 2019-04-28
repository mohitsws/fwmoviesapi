using FwData;
using FwData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FwInMemDb
{
    public class MovieRepository : IMovieRepository
    {
        public Movie UpsertRating(int id, int userId, int rating)
        {
            var movies = MoviesDB.Movies;
            if (!MoviesDB.IsValidUser(userId)) throw new ArgumentException("Invalid UserId");

            var movie = movies.Where(m => m.Id == id);
            if (movie.Count() != 1) throw new ArgumentException("Invalid Movie identifier");

            movie.First().AddRating(userId, rating);
            return movie.First();
        }
        public List<Movie> GetMovies(SearchRequest search = null)
        {
            var movies = MoviesDB.Movies;

            if (search == null)
                return movies;
            else
            {
                var moviesMatchingTitles = search.IsTitleFilterActivated ? MatchingTitle(search, movies) : movies;
                var moviesMatchingYoR = search.IsYearOfReleaseFilterActivated ? MatchingYearOfRelease(search, moviesMatchingTitles) : moviesMatchingTitles;
                var moviesMatchingGenres = search.IsGenreFilterActivated ? MatchingGenres(search, moviesMatchingYoR) : moviesMatchingYoR;

                var filteredMovies = search.IsUserFilterActivated ? MatchingUser(search, moviesMatchingGenres) : moviesMatchingGenres;

                IOrderedEnumerable<Movie> orderedCollection = null;
                if (search.Sorters.Count > 0)
                {
                    bool isFirst = true;
                    
                    foreach(var sortBy in search.Sorters)
                    {
                        if (isFirst)
                        {
                            orderedCollection = filteredMovies.OrderBy(m => m, sortBy);
                            isFirst = false;
                        }
                        else
                        {
                            orderedCollection = orderedCollection.ThenBy(m => m, sortBy);
                        }
                        
                    }
                }
                return orderedCollection != null ? orderedCollection.ToList() : filteredMovies;
            }
        }

        private List<Movie> MatchingUser(SearchRequest search, List<Movie> moviesToFilter)
        {
            var moviesReviewedByUser = new List<Movie>();
            moviesReviewedByUser.AddRange(moviesToFilter.Where(t => t.IsRatedByUser(search.UserId)).ToList().ConvertAll(s => s.CloneForUser(search.UserId)));            
            return moviesReviewedByUser;
        }

        private List<Movie> MatchingYearOfRelease(SearchRequest search, List<Movie> moviesToFilter)
        {
            var yorMovies = new List<Movie>();
            yorMovies.AddRange(moviesToFilter.Where(t => t.YearOfRelease.Equals(search.YearOfRelease)));
            return yorMovies;
        }
        private List<Movie> MatchingGenres(SearchRequest search, List<Movie> moviesToFilter)
        {
            var genreMovies = new List<Movie>();
            genreMovies.AddRange(moviesToFilter.Where(t => search.Genres.Contains(Genre.Any) || t.Genres.Intersect(search.Genres).Any()));
            return genreMovies;
        }

        private List<Movie> MatchingTitle(SearchRequest search, List<Movie> moviesToFilter)
        {
            var titledMovies = new List<Movie>();
                // Culture insensitive
                if (search.IsPartialSearchTitle)
                {
                    titledMovies.AddRange(moviesToFilter.Where(t => t.Title.ToLower().Contains(search.Title.ToLower())));
                }
                else
                {
                    titledMovies.AddRange(moviesToFilter.Where(t => t.Title.Equals(search.Title, StringComparison.OrdinalIgnoreCase)));
                }
            
            return titledMovies;
        }
    }
}
