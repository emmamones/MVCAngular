using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Persistance.DataModel; 
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Persistance.Migrations
{
    /// <summary>
    /// sealed does not allow to inherit from it.
    /// </summary>
    public sealed class DropIdentityModelConfiguration : DropCreateDatabaseAlways<ModelContext>
    {
        public DropIdentityModelConfiguration()
        {

        }
        ApplicationUser AddUserAndRole(ModelContext context)
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

            context.Users.AddOrUpdate(user);
            context.SaveChanges();
            return user;
        }


        protected override void Seed(ModelContext context)
        {
            var defaultUser = AddUserAndRole(context); 
        }

    }
}