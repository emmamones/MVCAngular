using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularMVCAuthentication.ViewModels
{
    public class RandomMovieVIewModel
    {
        public Movie Movie { get; set; }
        public List<Customer> Customers { get; set; }
    }
}