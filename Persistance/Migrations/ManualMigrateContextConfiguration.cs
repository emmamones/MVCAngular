namespace Persistance.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Persistance;
    using Persistance.DataModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class ManualMigrateContextConfiguration : DbMigrationsConfiguration<Persistance.PersistanceDBContext>
    {
        public ManualMigrateContextConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        ApplicationUser AddUserAndRole(PersistanceDBContext context)
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

        protected override void Seed(PersistanceDBContext context)
        {
            using (DbContextTransaction dbContextTransaction = context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {

                var defaultUser = AddUserAndRole(context);

                context.Eventos.AddOrUpdate(p => p.Title,
                      new Evento
                      { Id = 1, Title = "Debra Garcia", ApplicationUserName = defaultUser.UserName, Created = DateTime.Now, CreatedBy = "seed" },
                      new Evento
                      { Id = 2, Title = "Thorsten Weinrich", ApplicationUserName = defaultUser.UserName, Created = DateTime.Now, CreatedBy = "seed" },
                      new Evento
                      { Id = 3, Title = "Yuhong Li", ApplicationUserName = defaultUser.UserName, Created = DateTime.Now, CreatedBy = "seed" },
                      new Evento
                      { Id = 4, Title = "Jon Orton", ApplicationUserName = defaultUser.UserName, Created = DateTime.Now, CreatedBy = "seed" },
                      new Evento
                      { Id = 5, Title = "Diliana Alexieva-Bosseva", ApplicationUserName = defaultUser.UserName, Created = DateTime.Now, CreatedBy = "seed" }
                      );


                var genres = new List<Genre>
                {
                    new Genre {  Id = 1, Name = "Comedy" , Created = DateTime.Now, CreatedBy = "seed"   }
                    ,  new Genre { Id = 2, Name = "Horror", Created = DateTime.Now, CreatedBy = "seed"  }
                    ,  new Genre {  Id = 3, Name = "Drama", Created = DateTime.Now, CreatedBy = "seed" }
                    ,  new Genre { Id = 4, Name = "CSI", Created = DateTime.Now, CreatedBy = "seed"  }
                };

                genres.ForEach(s => context.Genres.AddOrUpdate(p => p.Id, s));

                var movies = new List<Movie>
                {
                       new Movie { Id = 1, Name = "Shrek!", DirectorName = "Spike Lee", ReleaseDate = DateTime.Now.AddDays(-15), Created = DateTime.Now, CreatedBy = "seed", NumberInStock = 5, Genre = genres.SingleOrDefault(c => c.Id == 1) },
                       new Movie { Id = 2, Name = "Tarzan", DirectorName = "Spike Lee", ReleaseDate = DateTime.Now.AddDays(-10), Created = DateTime.Now, CreatedBy = "seed", NumberInStock = 5, Genre = genres.SingleOrDefault(c => c.Id == 2) },
                       new Movie { Id = 3, Name = "Caliman", DirectorName = "Steven Spielberg", ReleaseDate = DateTime.Now.AddDays(-5), Created = DateTime.Now, CreatedBy = "seed", NumberInStock = 5, Genre = genres.SingleOrDefault(c => c.Id == 3) },
                       new Movie { Id = 4, Name = "Independence day", DirectorName = "James Cameron", ReleaseDate = DateTime.Now.AddDays(-1), Created = DateTime.Now, CreatedBy = "seed", NumberInStock = 5, Genre = genres.SingleOrDefault(c => c.Id == 4) } };

                movies.ForEach(m => context.Movies.AddOrUpdate(p => p.Id, m));

                var memberships = new List<MembershipType> {
                    new MembershipType {  Id = 1, SignUpFee = 0, DurationInMonths = 0, DiscountRate = 0, Created = DateTime.Now, CreatedBy = "seed" , Name = "Free" },
                    new MembershipType { Id = 2, SignUpFee = 30, DurationInMonths = 1, DiscountRate = 10, Created = DateTime.Now, CreatedBy = "seed" , Name = "One Month" },
                    new MembershipType { Id = 3, SignUpFee = 90, DurationInMonths = 3, DiscountRate = 15, Created = DateTime.Now, CreatedBy = "seed", Name = "Three Month"  },
                    new MembershipType { Id = 4, SignUpFee = 300, DurationInMonths = 12, DiscountRate = 20, Created = DateTime.Now, CreatedBy = "seed", Name = "Twelve Month"  }
                };

                memberships.ForEach(s => context.MembershipTypes.AddOrUpdate(p => p.Id, s));


              var Customers= new List<Customer> {
                           new Customer { Name = "Alberto", Id = 1, BirthDate = Convert.ToDateTime("01/01/1984"), IsSubscribedToNewsLetter = false, Created = DateTime.Now, CreatedBy = "seed", MembershipType = memberships.SingleOrDefault(c => c.Id == 1) },
                           new Customer { Name = "Federico", Id = 2, BirthDate = Convert.ToDateTime("01/01/1986 "), IsSubscribedToNewsLetter = true, Created = DateTime.Now, CreatedBy = "seed", MembershipType = memberships.SingleOrDefault(c => c.Id == 2) },
                           new Customer { Name = "Roberto", Id = 3, IsSubscribedToNewsLetter = true, Created = DateTime.Now, CreatedBy = "seed", MembershipType = memberships.SingleOrDefault(c => c.Id == 3) },
                           new Customer { Name = "Ismael", Id = 4, IsSubscribedToNewsLetter = true, Created = DateTime.Now, CreatedBy = "seed", MembershipType = memberships.SingleOrDefault(c => c.Id == 4) }
                         };

                Customers.ForEach(c => context.Customers.AddOrUpdate(p => p.Id, c));



                context.Rentals.AddOrUpdate(T => T.Id,
                           new Rental { Id = 1, Customer = Customers.Single(c => c.Id == 1), RentDate = Convert.ToDateTime("01/01/1984"),  ReturnDate = Convert.ToDateTime("01/01/2016"),  Movie = movies.Single(m=>m.Id==1), Created = DateTime.Now, CreatedBy = "seed" }
                         , new Rental { Id = 2, Customer = Customers.Single(c => c.Id == 1), RentDate = Convert.ToDateTime("01/01/1984"), ReturnDate = Convert.ToDateTime("01/01/2016"), Movie = movies.Single(m => m.Id == 2), Created = DateTime.Now, CreatedBy = "seed" }

                         );

            }
        }
    }
}
