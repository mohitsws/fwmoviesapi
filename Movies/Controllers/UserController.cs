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
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IFactory<IComparer<Movie>, SortAttributes> _comparerFactory;
        private readonly IMapper _mapper;

        public UserController(IMovieRepository repository, IFactory<IComparer<Movie>, SortAttributes> comparerFactory, IMapper mapper)
        {
            _movieRepository = repository;
            _comparerFactory = comparerFactory;
            _mapper = mapper;
        }

        [Route("{userId}/movies")]
        public IHttpActionResult Get([FromUri]int top, int userId)
        {
            if (top < 1)
            {
                return BadRequest("Please provide a valid positive integer number to fetch top n movies.");
            }
            var search = new SearchRequest().ByUser(userId)
                                            .SortBy(_comparerFactory.Get(SortAttributes.Rating))
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

        // POST api/values
        [Route("{id}/movies/{movieId}/ratings")]
        public IHttpActionResult Post(int id, int movieId, [FromBody]UserRating rating)
        {
            if(rating==null || rating.Rating<1 || rating.Rating > 5)
            {
                return BadRequest("Please provide a rating from 1-5.");
            }
            try
            {
                var movie = _movieRepository.UpsertRating(movieId, id, rating.Rating);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, ""));
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        // PUT api/values/5
        [Route("{id}/movies/{movieId}/ratings/")]
        public IHttpActionResult Put(int id, int movieId, [FromBody]UserRating rating)
        {
            if (rating == null || rating.Rating < 1 || rating.Rating > 5)
            {
                return BadRequest("Rating can only be updated from 1-5.");
            }
            try
            {
                var movie = _movieRepository.UpsertRating(movieId, id, rating.Rating);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, ""));
            }
            catch (ArgumentOutOfRangeException e)
            {
                return BadRequest(e.Message);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }
    }
}
