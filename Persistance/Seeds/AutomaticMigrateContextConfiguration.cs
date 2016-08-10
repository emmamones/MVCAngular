using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Persistance.Seeds
{
    /// <summary>
    /// sealed does not allow to inherit from it.
    /// </summary>
    public sealed class AutomaticMigrateContextConfiguration : DbMigrationsConfiguration<PersistanceContext>
    {
        public AutomaticMigrateContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
         

        protected override void Seed(PersistanceContext context)
        { 
              
        }
    }
}