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
        public static void SetInitializer(bool debug)
        {
            if (debug)
            {
                Console.WriteLine("Database.SetInitializer(DataContextConfiguration)");               
                System.Data.Entity.Database.SetInitializer<PersistanceContext>(new DataContextConfiguration());
            }
            else
            {
                try
                {
                    Console.WriteLine("Database.SetInitializer(MigrateDatabaseToLatestVersion)");
                    System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<PersistanceContext, DataContextMigrationConfiguration>(true));
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.ToString());
                }
               
            }
          

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
