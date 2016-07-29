using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class InitDataBase
    {
        public static void SetInitializer()
        {
            Debug.WriteLine("Database.SetInitializer(MigrateDatabaseToLatestVersion)");
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<ModelContext, DataContextConfiguration>(true));           
        }
    }
}
