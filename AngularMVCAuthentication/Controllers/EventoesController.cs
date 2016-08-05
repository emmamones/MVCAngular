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
using AngularMVCAuthentication.DataAccess;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace AngularMVCAuthentication.Controllers
{
    public class EventoesController : Controller
    {
        static object _InitLock = new object();
        private IDataRepository _Repository = null; 
        private ApplicationUser currentUser;
        private UserManager<ApplicationUser> manager;

        public virtual IDataRepository Repository
        {
            get
            {
                lock (_InitLock)
                {
                    if (_Repository == null)
                        _Repository = new DataRepository();
                    return _Repository;
                }
            }
        }
        public EventoesController()
        {
            _Repository = new DataRepository();
            var store = new UserStore<ApplicationUser>(Repository.GetContext());
            store.AutoSaveChanges = false;
            manager = new UserManager<ApplicationUser>(store);
            currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
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
        public ActionResult Create([Bind(Include = "Id,EventoId,Title,Date,Location,URL,Recommendation,Created,CreatedBy,Updated,UpdatedBy,RowVersion")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                 
                var store = new UserStore<ApplicationUser>(Repository.GetContext()); 
                manager = new UserManager<ApplicationUser>(store);
                currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());


                evento.ApplicationUser= currentUser;
                var context= System.Web.HttpContext.Current.GetOwinContext().Get<ModelContext>();

                context.Eventoes.Add(evento);

                context.SaveChanges();

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
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit([Bind(Include = "Id,EventoId,Title,Date,Location,URL,Recommendation,Created,CreatedBy,Updated,UpdatedBy,RowVersion")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                _Repository.Update(evento, currentUser.UserName);
                _Repository.SaveChanges();
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
            Evento evento = _Repository.Find<Evento>(id.Value);
            if (evento == null)
            {
                return HttpNotFound();
            }

            if (evento.Organizer != currentUser.UserName)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Only the Organizer can delete his event");
                //return RedirectToAction("Index");
            }

            return View(evento);
        }

        // POST: Eventoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
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
