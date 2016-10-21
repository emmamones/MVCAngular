

using Persistance.AspNetMigrations;
using Persistance.Migrations;
using System;
using System.Data.Entity;

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
                    System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<ModelContext, AutomaticIdentityModelConfiguration>(true));
                    System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<PersistanceDBContext, AutomaticPersistanceDBContextConfiguration>(true));
                    //System.Data.Entity.Database.SetInitializer<PersistanceDBContext>(new DropPersistanceDBContextConfiguration("user1@contoso.com"));
                    break;

                case 2:
                    try
                    {
                        Console.WriteLine("Database.SetInitializer(MigrateDatabaseToLatestVersion)");
                        System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<PersistanceDBContext, AutomaticPersistanceDBContextConfiguration>(true)); 
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
                        System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<PersistanceDBContext, ManualMigrateContextConfiguration>(true));
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
