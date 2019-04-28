using AutoMapper;
using FwData;
using FwData.Entities;
using Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Movies.Controllers
{
    [RoutePrefix("api/movies")]
    public class MoviesController : ApiController
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IFactory<IComparer<Movie>, SortAttributes> _comparerFactory;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepository repository, IFactory<IComparer<Movie>, SortAttributes> comparerFactory, IMapper mapper)
        {
            _movieRepository = repository;
            _comparerFactory = comparerFactory;
            _mapper = mapper;
        }

        [Route("")]
        public IHttpActionResult Get([FromUri]string sortBy, [FromUri]int top)
        {
            if (string.IsNullOrEmpty(sortBy) || top < 1)
            {
                return BadRequest("Please provide the valid SortBy attribute & a number to fetch top n movies.");
            }

            if (!Enum.TryParse(sortBy, true, out SortAttributes s)) // Support only rating as supported attribute
                return BadRequest("Movies can only be requested to sort based on Rating.");

            var search = new SearchRequest().SortBy(_comparerFactory.Get(SortAttributes.Rating))
                                            .SortBy(_comparerFactory.Get(SortAttributes.Title));

            var movies = _movieRepository.GetMovies(search);
            if (movies?.Count > 0)
            {
                var result = _mapper.Map<IEnumerable<MovieModel>>(movies)
                                        .Take(top);

                return Ok(result);
            }
            return NotFound();
        }

        [Route("")]
        public IHttpActionResult Get([FromUri] string title = "", [FromUri] int yearOfRelease = 0, [FromUri] string genres = "", [FromUri] bool partialTitle = false)
        {
            if (string.IsNullOrEmpty(title) && yearOfRelease == 0 && string.IsNullOrWhiteSpace(genres))
            {
                return BadRequest("At least one of the title, year of release or genre should be provided.");
            }

            var search = new SearchRequest().ByTitle(title, partialTitle).ByYearOfRelease(yearOfRelease).ByGenres(genres);

            var movies = _movieRepository.GetMovies(search);
            if (movies?.Count > 0)
            {
                var result = _mapper.Map<IEnumerable<MovieModel>>(movies);
                return Ok(result);
            }
            return NotFound();
        }
    }
}