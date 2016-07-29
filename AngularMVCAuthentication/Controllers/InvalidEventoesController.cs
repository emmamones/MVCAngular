using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AngularMVCAuthentication.Models;
using DataModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace AngularMVCAuthentication.Controllers
{
    public class InvalidEventoesController : Controller
    {
        private ApplicationDbContext dbAuthentication = new ApplicationDbContext();
        private ModelContext dbEventos = new ModelContext();
        private ApplicationUser currentUser;
        private UserManager<ApplicationUser> manager;

        public InvalidEventoesController()
        {
            dbAuthentication = new ApplicationDbContext();
            manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(dbAuthentication));
            currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

        }
        // GET: Eventoes
        public ActionResult Index()
        {
            currentUser = manager.FindById(User.Identity.GetUserId());
            return View(dbEventos.Eventoes.ToList());
        }

        // GET: Eventoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = dbEventos.Eventoes.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // GET: Eventoes/Create
        [Authorize(Roles = "canEdit")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eventoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult Create([Bind(Include = "EventoId,Title,Date,Location,URL,Recommendation,IsDeleted,RowVersion")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                dbEventos.Eventoes.Add(evento);
                dbEventos.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evento);
        }

        // GET: Eventoes/Edit/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = dbEventos.Eventoes.Find(id);
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
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit([Bind(Include = "EventoId,Title,Date,Location,URL,Recommendation,IsDeleted,RowVersion")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                dbEventos.Entry(evento).State = EntityState.Modified;
                dbEventos.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evento);
        }

        // GET: Eventoes/Delete/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = dbEventos.Eventoes.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            if (evento.Organizer != currentUser.UserName)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Only the Organizer can delete his event");
                //return RedirectToAction("Index");
            }
            //return Json(new { status = "error", message = "You are not the Organizer" });

            return View(evento);
        }

        // POST: Eventoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult DeleteConfirmed(int id)
        {
            Evento evento = dbEventos.Eventoes.Find(id);
            dbEventos.Eventoes.Remove(evento);
            dbEventos.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbAuthentication.Dispose();
                dbEventos.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
