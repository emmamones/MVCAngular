
using AngularMVCAuthentication.ViewModels;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Persistance.Core;
using Persistance;
using Persistance.DataModel;

namespace AngularMVCAuthentication.Controllers
{
    public class MoviesController : Controller
    {
        private PersistanceContext _context;
        public MoviesController()
        {
            _context = new PersistanceContext();
        }


        [Route("movies")]
        public ActionResult Index(int? pageIndex)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;


            List<Movie> pelis = null;
            using (var uW = new UnitOfWork(new PersistanceContext()))
            {
                pelis = uW.Movies.GetAllMoviesWithGender(pageIndex.Value).ToList();
            }


            return View(pelis);
        }

        [Route("movies/details/{id}")]
        public ActionResult Details(int? Id)
        {
            Movie peli = null;
            var viewModel = new RandomMovieVIewModel();

            if (!Id.HasValue)
                return View(viewModel);


            using (var uW = new UnitOfWork(new PersistanceContext()))
            {
                peli = uW.Movies.Get(Id.Value);
            }
            //  return View(peli);
            // return HttpNotFound("wee");
            // return new EmptyResult();
            //   return RedirectToAction("Index", "Home", new { page = 1, sortBy = "Name" });
            //ViewData["Verga"] = peli;
            //ViewBag.Verga = peli;
            // return Content($"pageIndex={pageIndex.Value }, sortBy = {sortBy}");
            List<Customer> customers = null;
            customers = _context.Customers.Include(c => c.MembershipType).ToList();

            viewModel = new RandomMovieVIewModel
            {
                Movie = peli,
                Customers = customers

            };
            return View(viewModel);
        }

        public ActionResult New()
        {
            IEnumerable<Genre> genres = null;

            using (var uW = new UnitOfWork(new PersistanceContext()))
            {
                genres = uW.Genres.GetAll();
            }

            var viewModelMovies = new MovieFormViewModel
            {
                Genres = genres
            };
            return View("MoviesForm", viewModelMovies);
        }
        public ActionResult Edit(int? Id)
        {
            //parameter binding
            //calling this Edit/1 works because The default routing its set to this Id Parameter name

            return Content($"id={Id.Value }");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(MovieFormViewModel vmModel)
        {
            if (!ModelState.IsValid)
            {
                vmModel.Genres = _context.Genres.ToList();
                return View("MoviesForm", vmModel);
            }


            if (vmModel.Id == 0)
            {
                var currentMovie = new Movie()
                {
                    Name = vmModel.Name,
                    ReleaseDate = vmModel.ReleaseDate,
                    ArrivalDate = vmModel.ArrivalDate,
                    DirectorName = vmModel.DirectorName,
                    InStock = vmModel.InStock,
                    GenreId = vmModel.GenreId,
                    Created = DateTime.Now,
                    CreatedBy = "Em"
                };

                using (var uW = new UnitOfWork(new PersistanceContext()))
                {
                    uW.Movies.Add(currentMovie, "Em");
                    uW.Complete();
                }

            }
            else
            {
                var currentMovie = new Movie();
                using (var uW = new UnitOfWork(new PersistanceContext()))
                {
                    currentMovie = uW.Movies.Get(vmModel.Id);

                    if (currentMovie==null)
                        return View("MoviesForm", vmModel);

                    currentMovie.Name = vmModel.Name;
                    currentMovie.ReleaseDate = vmModel.ReleaseDate;
                    currentMovie.ArrivalDate = vmModel.ArrivalDate;
                    currentMovie.DirectorName = vmModel.DirectorName;
                    currentMovie.InStock = vmModel.InStock;
                    currentMovie.GenreId = vmModel.GenreId;
                    currentMovie.Updated = DateTime.Now;
                    currentMovie.UpdatedBy = "Em"; 
                    
                    uW.Complete();
                }

            }



            return RedirectToAction("Index");
        }


        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int? year, int? month)
        {
            if (!year.HasValue)
                year = 1;

            if (!month.HasValue)
                month = 1;

            return Content($"year={year.Value }, month = {month}");
        }




    }
}