using AngularMVCAuthentication.Dtos;
using Persistance;
using Persistance.Core;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace AngularMVCAuthentication.Controllers.api
{
    public class NewRentalsController : ApiController
    {
        private PersistanceDBContext db = new PersistanceDBContext();

        [HttpPost]
        public IHttpActionResult New (NewRentalDto argNewRental)
        {
            if (string.IsNullOrEmpty(argNewRental.CustomerId))
                return BadRequest("Customer can not be empty");
             
            using (var uW = new UnitOfWork(new PersistanceDBContext()))
            {
                int idCustomer = Convert.ToInt32(argNewRental.CustomerId);
                var currentCustomer= uW.Customers.Get(idCustomer);

                Movie currentMovie = null;
                foreach (var idMovie in argNewRental.MovieIds)
                {
                    currentMovie = uW.Movies.Get(idMovie);
                    var newRental = new Rental()
                    { 
                        Movie = currentMovie,
                        Customer = currentCustomer,
                        Created = DateTime.Now,
                        CreatedBy = "Em",
                        RentDate=DateTime.Now,
                        ReturnDate=DateTime.Now.AddDays(8), 
                    };

                    uW.Rentals.Add(newRental, "Em");
                    uW.Complete();

                    currentMovie.NumberInStock--;
                    uW.Complete();
                }
                 
            }


            

            return Ok();

        }
    }
}
