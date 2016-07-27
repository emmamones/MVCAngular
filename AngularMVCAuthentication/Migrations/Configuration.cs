namespace AngularMVCAuthentication.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AngularMVCAuthentication.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<AngularMVCAuthentication.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        bool AddUserAndRole(AngularMVCAuthentication.Models.ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>
                (new RoleStore<IdentityRole>(context));

            rm.Create(new IdentityRole("canRead"));
            ir = rm.Create(new IdentityRole("canEdit"));

            var um = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));
            var user = new ApplicationUser()
            {
                UserName = "user1@contoso.com",
            };

            ir = um.Create(user, "P_assw0rd1");
            if (ir.Succeeded == false)
                return ir.Succeeded;

            ir = um.AddToRole(user.Id, "canEdit");
            return ir.Succeeded;
        }

        protected override void Seed(AngularMVCAuthentication.Models.ApplicationDbContext context)
        {

            AddUserAndRole(context);
            
            context.People.AddOrUpdate(p => p.FirstName,
                 new People
                 {
                     FirstName = "Debra Garcia",
                     Retired = true,
                     Email = "debra@example.com",
                 },
                  new People
                  {
                      FirstName = "Thorsten Weinrich",
                      Retired = true,
                      Email = "thorsten@example.com",
                  },
                  new People
                  {
                      FirstName = "Yuhong Li",
                      Retired = false,
                      Email = "yuhong@example.com",
                  },
                  new People
                  {
                      FirstName = "Jon Orton",
                      Retired = true,
                      Email = "jon@example.com",
                  },
                  new People
                  {
                      FirstName = "Diliana Alexieva-Bosseva",
                      Retired = false,
                      Email = "diliana@example.com",
                  }
                  );
        }
    }
}
