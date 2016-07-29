using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class ModelContext:DbContext
    {
        public const string _DatabaseName = "AngularMVC";
        public ModelContext() : base(_DatabaseName)
        {
            //if (!Database.Exists(_DatabaseName))
            //    Database.SetInitializer(new MigrateDatabaseToLatestVersion<ModelContext, DataContextConfiguration>());
        }

        public System.Data.Entity.DbSet<EventoExterno> EventoExterno { get; set; }


        public System.Data.Entity.DbSet<MyUserInfo> MyUserInfo { get; set; }
    }
}
