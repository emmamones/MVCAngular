using AngularMVCAuthentication.DataModel;
using AngularMVCAuthentication.Models;
using Persistance;
using Persistance.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AngularMVCAuthentication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (Debugger.IsAttached)
            {
                InitDataBase.SetInitializer();

                //var contPersistance = InitDataBase.GetEvenRepository();

                using (var uW = new UnitOfWork(new PersistanceContext()))
                {

                    var fisrtEvent = uW.Eventos.GetAll().FirstOrDefault();
                    uW.Eventos.Remove(fisrtEvent);
                    uW.Complete(); 

                }
                //System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<ModelContext, ApplicationDbContextConfiguration>(true));
                System.Data.Entity.Database.SetInitializer<ModelContext>(new ContextDropConfiguration());
            }
            else
            {
                System.Data.Entity.Database.SetInitializer<ModelContext>(null);
            }
        }
    }
}
