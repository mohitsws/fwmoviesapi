using FwData.Entities;
using System.Collections.Generic;

namespace FwData.Comparer
{
    public class MovieTitleComparer : IComparer<Movie>
    {
        public int Compare(Movie a, Movie b)
        {
            if (b != null)
            {
                var c = a.Title?.CompareTo(b.Title);
                return c ?? 1;
            }
            return -1;
        }
    }
}
