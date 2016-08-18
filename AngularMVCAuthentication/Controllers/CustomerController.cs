using Persistance;
using Persistance.DataModel;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularMVCAuthentication.Controllers
{
    public class CustomerController : Controller
    {
        private PersistanceContext _context;   
        public CustomerController()
        {
            _context = new PersistanceContext();

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult Index()
        {
            //context.Customers deffered execution this will not execute till the iteration in the view its executed.

            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

         
     
        public ActionResult Details(int? Id)
        {
            Customer custDetail = null;

            if (!Id.HasValue)
                return View(custDetail);

            var customers = _context.Customers.ToList();

            if (Id.Value > customers.Count | Id.Value < 0)
                return View(custDetail);

             custDetail = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(x => x.Id == Id);

            return View(custDetail);
        }
    }
}