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
            var customers = _context.Customers.Include(c=>c.MembershipType).ToList();
             
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

              custDetail = _context.Customers.SingleOrDefault(x => x.Id == Id);

            return View(custDetail);
        }
    }
}