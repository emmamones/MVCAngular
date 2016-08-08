using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AngularMVCAuthentication.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Details(int? Id)
        {
            Customer custDetail = null;

            if (!Id.HasValue)
                return View(custDetail);



            var customers = new List<Customer>() {
                new Customer() {  Name="Alberto",Id=1}
            , new Customer() {  Name="Federico",Id=2}
            ,  new Customer() {  Name="Roberto",Id=3}
             ,  new Customer() {  Name="Ismael",Id=4}
            };


            if (Id.Value > customers.Count | Id.Value < 0)
                return View(custDetail);


            custDetail = customers.Where(c => c.Id == Id.Value).FirstOrDefault();

            return View(custDetail);
        }
    }
}