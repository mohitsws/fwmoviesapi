using FwData.Entities;
using System;
using System.Collections.Generic;

namespace FwData.Comparer
{
    public class MovieSortComparerFactory : IFactory<IComparer<Movie>, SortAttributes>
    {
        public IComparer<Movie> Get(SortAttributes type)
        {
            switch (type)
            {
                case SortAttributes.Rating:
                    return new MovieRatingComparer();
                case SortAttributes.Title:
                    return new MovieTitleComparer();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
