using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Persistance.AspNetMigrations
{
    /// <summary>
    /// sealed does not allow to inherit from it.
    /// </summary>
    public sealed class DropPersistanceDBContextConfiguration : DropCreateDatabaseAlways<PersistanceDBContext>
    {
        string _defaultUserName;
        public DropPersistanceDBContextConfiguration(string argDefaultUserName)
        {
            _defaultUserName = argDefaultUserName;

        }



        protected override void Seed(PersistanceDBContext context)
        {
            var defaultUser = _defaultUserName;
            using (DbContextTransaction dbContextTransaction = context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {

                    context.Eventos.AddOrUpdate(p => p.Title,
                          new Evento
                          { Id = 1, Title = "Debra Garcia", ApplicationUserName = defaultUser, Created = DateTime.Now, CreatedBy = "seed" },
                          new Evento
                          { Id = 2, Title = "Thorsten Weinrich", ApplicationUserName = defaultUser, Created = DateTime.Now, CreatedBy = "seed" },
                          new Evento
                          { Id = 3, Title = "Yuhong Li", ApplicationUserName = defaultUser, Created = DateTime.Now, CreatedBy = "seed" },
                          new Evento
                          { Id = 4, Title = "Jon Orton", ApplicationUserName = defaultUser, Created = DateTime.Now, CreatedBy = "seed" },
                          new Evento
                          { Id = 5, Title = "Diliana Alexieva-Bosseva", ApplicationUserName = defaultUser, Created = DateTime.Now, CreatedBy = "seed" }
                          );

                    context.SaveChanges();
                    var genres = new List<Genre>
                {
                    new Genre {  Id = 1, Name = "Comedy" , Created = DateTime.Now, CreatedBy = "seed"   }
                    ,  new Genre { Id = 2, Name = "Horror", Created = DateTime.Now, CreatedBy = "seed"  }
                    ,  new Genre {  Id = 3, Name = "Drama", Created = DateTime.Now, CreatedBy = "seed" }
                    ,  new Genre { Id = 4, Name = "CSI", Created = DateTime.Now, CreatedBy = "seed"  }
                };


                    context.Genres.AddOrUpdate(genres.ToArray());
                    //genres.ForEach(g => context.Genres.AddOrUpdate(e => e.Id, g));

                    context.SaveChanges();
                    var movies = new List<Movie>
                {
                       new Movie { Id = 1, Name = "Shrek!",  DirectorName = "Spike Lee", ReleaseDate = DateTime.Now.AddDays(-15), ArrivalDate = DateTime.Now.AddDays(-20),Created = DateTime.Now, CreatedBy = "seed", NumberInStock = 5, Genre = genres.Single(c => c.Id == 1)  },
                       new Movie { Id = 2, Name = "Tarzan", DirectorName = "Spike Lee", ReleaseDate = DateTime.Now.AddDays(-10),ArrivalDate = DateTime.Now.AddDays(-20), Created = DateTime.Now, CreatedBy = "seed", NumberInStock = 5, Genre = genres.Single(c => c.Id == 2) },
                       new Movie { Id = 3, Name = "Caliman", DirectorName = "Steven Spielberg", ReleaseDate = DateTime.Now.AddDays(-5),ArrivalDate = DateTime.Now.AddDays(-20), Created = DateTime.Now, CreatedBy = "seed", NumberInStock = 5, Genre = genres.Single(c => c.Id == 3) },
                       new Movie { Id = 4, Name = "Independence day", DirectorName = "James Cameron", ReleaseDate = DateTime.Now.AddDays(-1),ArrivalDate = DateTime.Now.AddDays(-20), Created = DateTime.Now, CreatedBy = "seed", NumberInStock = 5, Genre = genres.Single(c => c.Id == 4) } };

                    //movies.ForEach(m => context.Movies.AddOrUpdate(p => p.Id, m));
                    context.Movies.AddOrUpdate(movies.ToArray());
                    context.SaveChanges();
                    var memberships = new List<MembershipType> {
                    new MembershipType {  Id = 1, SignUpFee = 0, DurationInMonths = 0, DiscountRate = 0, Created = DateTime.Now, CreatedBy = "seed" , Name = "Free" },
                    new MembershipType { Id = 2, SignUpFee = 30, DurationInMonths = 1, DiscountRate = 10, Created = DateTime.Now, CreatedBy = "seed" , Name = "One Month" },
                    new MembershipType { Id = 3, SignUpFee = 90, DurationInMonths = 3, DiscountRate = 15, Created = DateTime.Now, CreatedBy = "seed", Name = "Three Month"  },
                    new MembershipType { Id = 4, SignUpFee = 300, DurationInMonths = 12, DiscountRate = 20, Created = DateTime.Now, CreatedBy = "seed", Name = "Twelve Month"  }
                };

                    memberships.ForEach(s => context.MembershipTypes.AddOrUpdate(p => p.Id, s));
                    context.SaveChanges();


                    //var Customers = Customer.Create(memberships);

                    //if (!context.Customers.Any())
                    //{
                    //    context.Customers.AddOrUpdate(Customers);
                    //    context.SaveChanges();
                    //}

                    var customers = Customer.Create(memberships.ToArray());

                    customers.ForEach(c => context.Customers.AddOrUpdate(e => e.Id, c));
                    context.SaveChanges();

                    context.Rentals.AddOrUpdate(T => T.TimeStamp,
                               new Rental { Customer = customers.Single(c => c.Id == 1), RentDate = Convert.ToDateTime("01/01/1984"), ReturnDate = Convert.ToDateTime("01/01/2016"), Movie = movies.Single(m => m.Id == 1), Created = DateTime.Now.AddHours(-1), TimeStamp = DateTime.Now.AddHours(-1).ToString(), CreatedBy = "seed" }
                             , new Rental { Customer = customers.Single(c => c.Id == 2), RentDate = Convert.ToDateTime("01/01/1984"), ReturnDate = Convert.ToDateTime("01/01/2016"), Movie = movies.Single(m => m.Id == 2), TimeStamp = DateTime.Now.ToString(), Created = DateTime.Now, CreatedBy = "seed" }

                             );

                    context.SaveChanges();

                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
        }

    }
}