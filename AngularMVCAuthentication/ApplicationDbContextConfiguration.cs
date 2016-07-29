
using AngularMVCAuthentication.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace AngularMVCAuthentication
{


    public sealed class ApplicationDbContextConfiguration : DbMigrationsConfiguration<AngularMVCAuthentication.Models.ApplicationDbContext>
    {
        public ApplicationDbContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        ApplicationUser AddUserAndRole(AngularMVCAuthentication.Models.ApplicationDbContext context)
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
                UserName = "user1@contoso.com"
            };

            ir = um.Create(user, "P_assw0rd1");
            if (ir.Succeeded == false)
                return null;

            ir = um.AddToRole(user.Id, "canEdit");
            return user;
        }

        protected override void Seed(AngularMVCAuthentication.Models.ApplicationDbContext context)
        {



            var defaultUser = AddUserAndRole(context);

            context.Eventoes.AddOrUpdate(p => p.Title,
                  new Evento
                  {
                      Title = "Debra Garcia",
                      ApplicationUser = defaultUser
                  },
                  new Evento
                  {
                      Title = "Thorsten Weinrich",
                      ApplicationUser = defaultUser
                  },
                  new Evento
                  {
                      Title = "Yuhong Li",
                      ApplicationUser = defaultUser
                  },
                  new Evento
                  {
                      Title = "Jon Orton",
                      ApplicationUser = defaultUser
                  },
                  new Evento
                  {
                      Title = "Diliana Alexieva-Bosseva",
                      ApplicationUser = defaultUser
                  }
                  ); 

        }
    }
}
