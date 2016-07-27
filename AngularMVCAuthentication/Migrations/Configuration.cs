namespace AngularMVCAuthentication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AngularMVCAuthentication.Models;
    internal sealed class Configuration : DbMigrationsConfiguration<AngularMVCAuthentication.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AngularMVCAuthentication.Models.ApplicationDbContext context)
        {
            context.People.AddOrUpdate(p => p.FirstName,
       new Person
       {
           FirstName = "Debra Garcia", 
           Retired=true,
           Email = "debra@example.com",
       },
        new Person
        {
            FirstName = "Thorsten Weinrich",
            Retired = true,
            Email = "thorsten@example.com",
        },
        new Person
        {
            FirstName = "Yuhong Li",
            Retired = false,
            Email = "yuhong@example.com",
        },
        new Person
        {
            FirstName = "Jon Orton",
            Retired = true,
            Email = "jon@example.com",
        },
        new Person
        {
            FirstName = "Diliana Alexieva-Bosseva",
            Retired = false,
            Email = "diliana@example.com",
        }
        );
        }
    }
}
