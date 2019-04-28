using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FwData.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public int RunningTime { get; set; }
        public List<Genre> Genres { get; set; }
        private Dictionary<int, int> Ratings { get; set; } = new Dictionary<int, int>(); // Need to make thread safe 

        public bool IsRatedByUser(int userId)
        {
            return Ratings.ContainsKey(userId);
        }
        public int UserRating(int userId)
        {
            return Ratings[userId];  //TODO: Currently assumes the existence
        }
        public double AverageRating
        {
            get
            {
                double sum = Ratings.Sum(x => x.Value);
                return (sum / Ratings.Count);
            }
        }

        public void AddRating(int userId, int rating)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentOutOfRangeException("Rating must be between 1-5.");

            Ratings[userId] = rating;
        }
        public Movie CloneForUser(int userId)
        {
            var newMovie = new Movie()
            {
                Id = this.Id,
                Title = this.Title,
                YearOfRelease = this.YearOfRelease,
                RunningTime = this.RunningTime,
                Genres = this.Genres,
                Ratings = new Dictionary<int, int>()
            };
            newMovie.AddRating(userId, Ratings[userId]);
            return newMovie;
        }
    }
}
