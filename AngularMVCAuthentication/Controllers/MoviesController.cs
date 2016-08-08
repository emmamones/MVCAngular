
using AngularMVCAuthentication.ViewModels;
using Persistance;
using Persistance.Core;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularMVCAuthentication.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        public ActionResult BestMovies()
        {
            List<Movie> pelis = new List<Movie>();
            using (var uW = new UnitOfWork(new PersistanceContext()))
            {

                pelis = uW.Movies.GetlastMovies(5).ToList();
            }

            return View(pelis);
        }

        [Route("movies")]
        public ActionResult Random()
        {
            Movie peli = null;
            using (var uW = new UnitOfWork(new PersistanceContext()))
            { 
                peli = uW.Movies.GetlastMovies(5).FirstOrDefault(); 
            }
            //  return View(peli);
            // return HttpNotFound("wee");
            // return new EmptyResult();
            //   return RedirectToAction("Index", "Home", new { page = 1, sortBy = "Name" });
            //ViewData["Verga"] = peli;
            //ViewBag.Verga = peli;

            var customers = new List<Customer>() {
                new Customer() {  Name="Alberto",Id=1}
            , new Customer() {  Name="Federico",Id=2}
            ,  new Customer() {  Name="Roberto",Id=3}
             ,  new Customer() {  Name="Ismael",Id=4}
            };



            var viewModel = new RandomMovieVIewModel
            {
                Movie = peli,
                Customers = customers
        
            };
            return View(viewModel);
        }

       

        public ActionResult Edit(int? Id)
        {
            //parameter binding
            //calling this Edit/1 works because The default routing its set to this Id Parameter name

            return Content($"id={Id.Value }");
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;

            if ( string.IsNullOrEmpty(sortBy))
                sortBy = "Name";

            //par1\ameter binding
            return Content($"pageIndex={pageIndex.Value }, sortBy = {sortBy}");

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