using Persistance.Core;
using Persistance.DataModel;
using Persistance.Repositories;
using Persistance.Seeds;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance
{
    public class InitDataBase
    {
        public static void SetInitializer()
        {
            Debug.WriteLine("Database.SetInitializer(MigrateDatabaseToLatestVersion)");
            //System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<ModelContext, DataContextConfiguration>(true));           
            System.Data.Entity.Database.SetInitializer<PersistanceContext>(new DataContextConfiguration());

        }

        //public static EventoRepository GetEvenRepository()
        //{
        //    //ModelContext context = new ModelContext();
        //    //return  new EventoRepository(context);

        //    using (var uW = new UnitOfWork(new PersistanceContext()))
        //    {

        //        var Events = uW.Eventos.GetAll();

        //    }
        //}
    }
}
