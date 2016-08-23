using AngularMVCAuthentication.Dtos;
using AutoMapper;
using Persistance;
using Persistance.Core;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngularMVCAuthentication.Controllers.Api
{
    public class GenresController : ApiController
    {

        [HttpGet]
        //GET /api/genres
        //[Route("api/movies/genres")]
        public IHttpActionResult Genres()
        {
            IEnumerable<GenreDto> result = null;
            using (var uW = new UnitOfWork(new PersistanceContext()))
            {
                result = uW.Genres.GetAll().ToList().Select(Mapper.Map<Genre, GenreDto>);

                if (result == null)
                    return NotFound();
            }
             
            return Ok(result);
        }

        [HttpGet]
        //[Route("api/movies/genre/{id}")]
        public IHttpActionResult GetGenre(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            GenreDto result = null;
            using (var uW = new UnitOfWork(new PersistanceContext()))
            {
                var foundeGenre = uW.Genres.Get(id.Value);
                if (foundeGenre == null)
                    return NotFound();

                result = Mapper.Map<Genre, GenreDto>(foundeGenre);
            }
              
            return Ok(result);
        }

        [HttpPost]
        //[Route("api/movies/createGenre")]
        public IHttpActionResult CreateGenre(GenreDto genreDto)
        {

            if (!ModelState.IsValid)
                return BadRequest("genre invalid");

            using (var uW = new UnitOfWork(new PersistanceContext()))
            {
                var genre = Mapper.Map<GenreDto, Genre>(genreDto);

                uW.Genres.Add(genre, "em");
                uW.Complete();
                genreDto.Id = genre.Id;
            }

            // var testUri = new Uri($"{ Request.RequestUri }/{genreDto.Id}");
            return Created(new Uri($"{ Request.RequestUri }/{genreDto.Id}"), genreDto);
        }

        [HttpPut]
        //[Route("api/movies/updateGenre/{id}/{genreDto}")]
        public IHttpActionResult UpdateGenre(int id, GenreDto genreDto)
        {

            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
             

            using (var uW = new UnitOfWork(new PersistanceContext()))
            {
                var genreInDB = uW.Genres.Get(id);

                if (genreInDB == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                //here will track changes from the genreDTO
                Mapper.Map(genreDto, genreInDB);

                uW.Complete();

            }

            return Ok(genreDto);
        }

        [HttpDelete]
        public void DeleteGenre(int id)
        {

            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            using (var uW = new UnitOfWork(new PersistanceContext()))
            {
                var validGenre = uW.Genres.Get(id);

                if (validGenre == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                uW.Genres.Remove(validGenre);

                uW.Complete();

            }
        }

        [HttpGet]
        [Route("api/movies/byGenre/{id}")]
        public IEnumerable<MovieDto> ByGenre(int? id)
        {
            if (!id.HasValue)
                id = 1;

            IEnumerable<MovieDto> pelis = null;
            using (var uW = new UnitOfWork(new PersistanceContext()))
            {
                pelis = uW.Movies.ByGenre(id.Value).ToList().Select(Mapper.Map<Movie, MovieDto>);
            }

            if (pelis == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return pelis;
        }
    }
}
