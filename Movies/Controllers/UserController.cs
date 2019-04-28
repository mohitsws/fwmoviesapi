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
            try
            {
                var movie = _movieRepository.UpsertRating(movieId, id, rating.Rating);
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, ""));
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
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
            var movie = _movieRepository.UpsertRating(movieId, id, rating.Rating);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.OK, ""));
        }
    }
}
