using Microsoft.AspNet.Identity.EntityFramework;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class PersistanceDBContext: DbContext
    {
        public const string _DatabaseName = "AngularMVCJourny";

        public PersistanceDBContext(string argConnectionString) : base(argConnectionString)
        {
            //if (!Database.Exists(_DatabaseName))
            //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<ModelContext, DataContextConfiguration>());
        }
        public PersistanceDBContext() : base(_DatabaseName)
        {
            //if (!Database.Exists(_DatabaseName))
            //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<ModelContext, DataContextConfiguration>());
        }
       
        public System.Data.Entity.DbSet<Evento> Eventos { get; set; }
        public System.Data.Entity.DbSet<Persistance.DataModel.MembershipType> MembershipTypes { get; set; }
        public System.Data.Entity.DbSet<Persistance.DataModel.Genre> Genres { get; set; }
        public System.Data.Entity.DbSet<Persistance.DataModel.Movie> Movies { get; set; }
        public System.Data.Entity.DbSet<Persistance.DataModel.Customer> Customers { get; set; }           
        public System.Data.Entity.DbSet<Persistance.DataModel.Rental> Rentals { get; set; }

    }
}
