using FwData.Entities;
using System.Collections.Generic;

namespace FwData
{
    public interface IMovieRepository
    {
        List<Movie> GetMovies(SearchRequest search = null);
        Movie UpsertRating(int id, int userId, int rating);
    }
}
