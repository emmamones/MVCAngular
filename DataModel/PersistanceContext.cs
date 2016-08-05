using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class PersistanceContext:DbContext
    {
        public const string _DatabaseName = "AngularMVC";

        public PersistanceContext(string argConnectionString) : base(argConnectionString)
        {
            //if (!Database.Exists(_DatabaseName))
            //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<ModelContext, DataContextConfiguration>());
        }
        public PersistanceContext() : base(_DatabaseName)
        {
            //if (!Database.Exists(_DatabaseName))
            //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<ModelContext, DataContextConfiguration>());
        }

        public System.Data.Entity.DbSet<Evento> Evento { get; set; }


        public System.Data.Entity.DbSet<MyUserInfo> MyUserInfo { get; set; }
    }
}
