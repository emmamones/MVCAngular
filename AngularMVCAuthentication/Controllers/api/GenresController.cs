using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Persistance;
using Persistance.DataModel;
using Persistance.Core;
using AngularMVCAuthentication.Dtos;
using AutoMapper;

namespace AngularMVCAuthentication.Controllers.api
{
    public class GenresController : ApiController
    {
        private PersistanceContext db = new PersistanceContext();

        // GET: api/Genres
        [HttpGet]
        public IHttpActionResult GetGenres()
        {
            IEnumerable<GenreDto> genres = null;
            using (var uW = new UnitOfWork(new PersistanceContext()))
            {
                genres = uW.Genres.GetAll().ToList().Select(Mapper.Map<Genre, GenreDto>);

            }
            return Ok(genres);
        }

        // GET: api/Genres/5
        [HttpGet]
        [ResponseType(typeof(GenreDto))]
        public IHttpActionResult GetGenre(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            GenreDto result = null;
            using (var uW = new UnitOfWork(new PersistanceContext()))
            {
                var genre = uW.Genres.Get(id.Value);
                result = Mapper.Map<Genre, GenreDto>(genre);
            }


            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // PUT: api/Genres/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult UpdateGenre(int id, GenreDto genreDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (id != genreDto.Id)
                return BadRequest();

            try
            {
                using (var uW = new UnitOfWork(new PersistanceContext()))
                {
                    var customerInDb = uW.Genres.Get(id);

                    if (customerInDb == null)
                        return NotFound();

                    Mapper.Map<GenreDto, Genre>(genreDto, customerInDb);

                    uW.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Genres
        [ResponseType(typeof(GenreDto))]
        [HttpPost] 
        public IHttpActionResult CreateGenre(GenreDto genreDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var genre = Mapper.Map<GenreDto, Genre>(genreDto);
                using (var uW = new UnitOfWork(new PersistanceContext()))
                {
                    uW.Genres.Add(genre, "Em");
                    uW.Complete();
                    genreDto.Id = genre.Id;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            } 

            return CreatedAtRoute("api", new { id = genreDto.Id }, genreDto);
        }

        // DELETE: api/Genres/5
        [ResponseType(typeof(Genre))]
        public IHttpActionResult DeleteGenre(int id)
        {
            Genre genre = db.Genres.Find(id);
            if (genre == null)
            {
                return NotFound();
            }

            db.Genres.Remove(genre);
            db.SaveChanges();

            return Ok(genre);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GenreExists(int id)
        {
            return db.Genres.Count(e => e.Id == id) > 0;
        }
    }
}