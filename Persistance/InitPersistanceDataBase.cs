
using Persistance.Core;
using Persistance.DataModel;
using Persistance.Migrations;
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
    public class InitPersistanceDataBase
    {
        public static void SetInitializer(int argTypeInitializer)
        {
            switch (argTypeInitializer)
            {
                case 1://debug
                    Console.WriteLine("Database.SetInitializer(DataContextConfiguration)");
                    System.Data.Entity.Database.SetInitializer<PersistanceContext>(new DropContextConfiguration());
                    break;

                case 2:
                    try
                    {
                        Console.WriteLine("Database.SetInitializer(MigrateDatabaseToLatestVersion)");
                        System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<PersistanceContext, AutomaticMigrateContextConfiguration>(true)); 
                    }
                    catch (Exception ex)
                    { 
                        Console.WriteLine(ex.ToString());
                    } 

                    break;

                case 3:
                    try
                    {
                        Console.WriteLine("Database.SetInitializer(MigrateDatabaseToLatestVersion)");
                        System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<PersistanceContext, ManualMigrateContextConfiguration>(true));
                    }
                    catch (Exception ex)
                    { 
                        Console.WriteLine(ex.ToString());
                    }
                    break;
            }
        } 
    }
}
