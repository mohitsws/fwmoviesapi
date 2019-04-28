using FwData.Entities;
using System.Collections.Generic;

namespace FwData.Comparer
{
    public class MovieRatingComparer : IComparer<Movie>
    {
        public int Compare(Movie a, Movie b)
        {
            // Descending
            return b.AverageRating.CompareTo(a.AverageRating);
        }
    }
}
