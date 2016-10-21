using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Persistance;
using Persistance.DataModel;
using Persistance.Core;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity; 
using Microsoft.AspNet.Identity.Owin;

namespace AngularMVCAuthentication.Controllers
{
    public class EventosController : Controller
    {
 
        private ApplicationUser currentUser;
        private UserManager<ApplicationUser> manager;
        // GET: Eventos
        public ActionResult Index()
        {
            using (var uW = new UnitOfWork(new PersistanceDBContext()))
            {

                return View(uW.Eventos.GetAll().ToList());
            }
        }

        // GET: Eventos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = null;
            using (var uW = new UnitOfWork(new PersistanceDBContext()))
            {
                evento = uW.Eventos.Get(id.Value);
                if (evento == null)
                {
                    return HttpNotFound();
                }
            }
            return View(evento);
        }

        // GET: Eventos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult Create([Bind(Include = "Id,Title,DateEvent,Location,URL,Recommendation")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                using (var uW = new UnitOfWork(new PersistanceDBContext()))
                {

                    var store = new UserStore<ApplicationUser>(uW.Eventos.GetContext());
                    manager = new UserManager<ApplicationUser>(store);
                    currentUser = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());
                    var usName = currentUser.UserName;

                   // evento.ApplicationUser = new MyUserInfo() { UserName = usName };
                    evento.CreatedBy = usName;
                    evento.Created = DateTime.Now;

                    var context = System.Web.HttpContext.Current.GetOwinContext().Get<PersistanceDBContext>();

                    uW.Eventos.Add(evento, usName);

                    uW.Complete();

                    return RedirectToAction("Index");
                }


            }

            return View(evento);
        }

        // GET: Eventos/Edit/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Evento evento = null;
            using (var uW = new UnitOfWork(new PersistanceDBContext()))
            {
                evento = uW.Eventos.Get(id.Value);
                if (evento == null)
                {
                    return HttpNotFound();
                }
            }
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit([Bind(Include = "Id,Title,DateEvent,Location,URL,Recommendation,Created,CreatedBy,Updated,UpdatedBy,RowVersion")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(evento).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evento);
        }

        // GET: Eventos/Delete/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = null;
            using (var uW = new UnitOfWork(new PersistanceDBContext()))
            {
                evento = uW.Eventos.Get(id.Value);
                if (evento == null)
                {
                    return HttpNotFound();
                }
            }
            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult DeleteConfirmed(int id)
        {
            Evento evento = null;
            using (var uW = new UnitOfWork(new PersistanceDBContext()))
            {
                evento = uW.Eventos.Get(id);
                uW.Eventos.Remove(evento);
                uW.Complete();
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                
            }
            base.Dispose(disposing);
        }
    }
}
