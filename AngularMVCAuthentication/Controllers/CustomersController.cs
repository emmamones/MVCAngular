using Persistance;
using Persistance.DataModel;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AngularMVCAuthentication.ViewModels;

namespace AngularMVCAuthentication.Controllers
{
    public class CustomersController : Controller
    {
        private PersistanceContext _context;
        public CustomersController()
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

            custDetail = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(x => x.Id == Id);

            return View(custDetail);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModelCustomer = new CustomerFromViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModelCustomer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Save(CustomerFromViewModel viewModel)
        {
            

            if (!ModelState.IsValid)
            {
                viewModel.MembershipTypes = _context.MembershipTypes.ToList();
                return View("CustomerForm", viewModel);
            }

            var customer = new Customer();
            if (viewModel.Id == 0)
            {
                customer.Name = viewModel.Name;
                customer.BirthDate = viewModel.BirthDate;
                customer.IsSubscribedToNewsLetter = viewModel.IsSubscribedToNewsLetter;
                customer.MembershipTypeId = viewModel.MembershipTypeId;
                customer.Created = DateTime.Now;
                customer.CreatedBy = "Em";

                _context.Customers.Add(customer);
            }
            else
            {
                customer = _context.Customers.Single(c => c.Id == viewModel.Id);

                if (customer == null)
                    return View("CustomerForm", viewModel);


                customer.Name = viewModel.Name;
                customer.BirthDate = viewModel.BirthDate;
                customer.IsSubscribedToNewsLetter = viewModel.IsSubscribedToNewsLetter;
                customer.MembershipTypeId = viewModel.MembershipTypeId;
                customer.Updated = DateTime.Now;
                customer.UpdatedBy = "EM";
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {

            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var vm = new CustomerFromViewModel()
            {
                Name = customer.Name,
                BirthDate = customer.BirthDate,
                IsSubscribedToNewsLetter = customer.IsSubscribedToNewsLetter,
                MembershipTypeId = customer.MembershipTypeId,
                MembershipTypes = _context.MembershipTypes.ToList()

            };


            return View("CustomerForm", vm);

        }
    }
}