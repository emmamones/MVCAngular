using Persistance.DataModel;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Persistance.Seeds
{
    /// <summary>
    /// sealed does not allow to inherit from it.
    /// </summary>
    public sealed class DataContextMigrationConfiguration : DbMigrationsConfiguration<PersistanceContext>
    {
        public DataContextMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        MyUserInfo AddUserAndRole(PersistanceContext context)
        {
            var user = new MyUserInfo()
            {
                Id = 1,
                Created = DateTime.Now,
                CreatedBy = "seed",
                UserName = "user1@contoso.com",
                FirstName = "Emmanuel",
                LastName = "Lohora",
                Eventos = new System.Collections.Generic.List<Evento>()
            };

            return user;
        }


        protected override void Seed(PersistanceContext context)
        {
        }
    }
}