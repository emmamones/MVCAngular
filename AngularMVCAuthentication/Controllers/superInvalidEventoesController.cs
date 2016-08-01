using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AngularMVCAuthentication;
using AngularMVCAuthentication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using AngularMVCAuthentication.DataAccess;

namespace AngularMVCAuthentication.Controllers
{
    public class superInvalidEventoesController : Controller
    {

         private IDataRepository _Repository = null;
        private ModelContext dbAuthentication;
        private ApplicationUser currentUser;
        private UserManager<ApplicationUser> manager;

        public superInvalidEventoesController()
        {
            dbAuthentication = new ModelContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbAuthentication));
            currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
            _Repository = new DataRepository();

        }

        // GET: Eventoes
        public ActionResult Index()
        {
            return View(_Repository.Read<Evento>().ToList());
        }

        // GET: Eventoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = _Repository.Find<Evento>(id.Value);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // GET: Eventoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eventoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EventoId,Title,Date,Location,URL,Recommendation,Created,CreatedBy,Updated,UpdatedBy,RowVersion")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                _Repository.Create(evento,"CAmaras");
                _Repository.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evento);
        }

        // GET: Eventoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = _Repository.Find<Evento>(id.Value);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Eventoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventoId,Title,Date,Location,URL,Recommendation,Created,CreatedBy,Updated,UpdatedBy,RowVersion")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                _Repository.Update(evento,"editovato");
                _Repository.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evento);
        }

        // GET: Eventoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = _Repository.Find<Evento>(id.Value);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Eventoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evento evento = _Repository.Find<Evento>(id);
            _Repository.Delete(evento);
            _Repository.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _Repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
