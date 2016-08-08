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
                Console.WriteLine("Debugger.IsAttached");
                InitPersistanceDataBase.SetInitializer(true);
                System.Data.Entity.Database.SetInitializer<ModelContext>(new DropContextConfiguration());
            }
            else
            {
                InitPersistanceDataBase.SetInitializer(false);
                System.Data.Entity.Database.SetInitializer<ModelContext>(null); 
            }
        }
    }
}
