using FwData.Entities;
using System;
using System.Collections.Generic;

namespace FwData
{
    public class SearchRequest
    {
        public SearchRequest()
        {
            Genres = new List<Genre> { Genre.Any };
        }

        // Search by Title, Release year or Genres
        public string Title { get; private set; } = string.Empty;
        public int YearOfRelease { get; private set; } = int.MinValue;
        public List<Genre> Genres { get; private set; } 
        public bool IsPartialSearchTitle { get; private set; } = false;
        public bool IsTitleFilterActivated { get; private set; } = false;
        public bool IsYearOfReleaseFilterActivated { get; private set; } = false;
        public bool IsGenreFilterActivated { get; private set; } = false;

        // Allow Sort By & top
        public List<IComparer<Movie>> Sorters { get; private set; } = new List<IComparer<Movie>>();

        // Search by Userid
        public bool IsUserFilterActivated { get; private set; } = false;
        public int UserId { get; private set; } = int.MinValue;

        public SearchRequest SortBy(IComparer<Movie> c)
        {
            Sorters.Add(c);
            return this;
        }

        public SearchRequest ByUser(int userId)
        {
            if (userId > 0)
            {
                UserId = userId;
                IsUserFilterActivated = true;
            }
            return this;
        }


        public SearchRequest ByTitle(string title, bool isPartial = false)
        {
            if (!string.IsNullOrEmpty(title))
            {
                Title = title;
                IsPartialSearchTitle = isPartial;
                IsTitleFilterActivated = true;
            }
            return this;
        }

        public SearchRequest ByYearOfRelease(int yor)
        {
            if (yor > 0)
            {
                YearOfRelease = yor;
                IsYearOfReleaseFilterActivated = true;
            }
            return this;
        }
        public SearchRequest ByGenres(string csvGenres)
        {
            if (!string.IsNullOrWhiteSpace(csvGenres))
            {
                var genres = csvGenres.Split(',');
                Genres = new List<Genre>(genres.Length);
                foreach (var g in genres)
                {
                    if (Enum.TryParse(g, true, out Genre genre))
                        Genres.Add(genre);
                }
                //if (Genres.Count > 0)                // if enabled- Ignores the invalid output
                IsGenreFilterActivated = true;
            }
            return this;
        }
    }
}
