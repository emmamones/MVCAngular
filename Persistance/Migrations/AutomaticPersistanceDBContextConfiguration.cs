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
    public sealed class AutomaticPersistanceDBContextConfiguration : DbMigrationsConfiguration<PersistanceDBContext>
    {
        string _defaultUserName;
        public AutomaticPersistanceDBContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            _defaultUserName = "user1@contoso.com";
        }



        protected override void Seed(PersistanceDBContext context)
        {
            var defaultUser = _defaultUserName;
            using (DbContextTransaction dbContextTransaction = context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                try
                {
                    if (!context.Eventos.Any())
                    {
                        context.Eventos.AddOrUpdate(p => p.Title,
                          new Evento
                          { Title = "Debra Garcia", ApplicationUserName = defaultUser, Created = DateTime.Now, CreatedBy = "seed" },
                          new Evento
                          { Title = "Thorsten Weinrich", ApplicationUserName = defaultUser, Created = DateTime.Now, CreatedBy = "seed" },
                          new Evento
                          { Title = "Yuhong Li", ApplicationUserName = defaultUser, Created = DateTime.Now, CreatedBy = "seed" },
                          new Evento
                          { Title = "Jon Orton", ApplicationUserName = defaultUser, Created = DateTime.Now, CreatedBy = "seed" },
                          new Evento
                          { Title = "Diliana Alexieva-Bosseva", ApplicationUserName = defaultUser, Created = DateTime.Now, CreatedBy = "seed" }
                          );

                        context.SaveChanges();
                    }

                    var genres = new List<Genre>();
                    if (!context.Genres.Any())
                    {
                        genres = new List<Genre>
                                    {
                                        new Genre { Name = "Comedy" , Created = DateTime.Now, CreatedBy = "seed"   }
                                        ,  new Genre { Name = "Horror", Created = DateTime.Now, CreatedBy = "seed"  }
                                        ,  new Genre { Name = "Drama", Created = DateTime.Now, CreatedBy = "seed" }
                                        ,  new Genre { Name = "CSI", Created = DateTime.Now, CreatedBy = "seed"  }
                                    };

                        // context.Genres.AddOrUpdate(genres.ToArray());
                        genres.ForEach(g => context.Genres.AddOrUpdate(e => e.Name, g));
                        context.SaveChanges();
                    }
                

                    var movies = new List<Movie>();
                    if (!context.Movies.Any())
                    {
                        movies = new List<Movie>
                                    {
                                       new Movie {  Name = "Shrek!",  DirectorName = "Spike Lee", ReleaseDate = DateTime.Now.AddDays(-15), ArrivalDate = DateTime.Now.AddDays(-20),Created = DateTime.Now, CreatedBy = "seed", NumberInStock = 5, GenreId=1, Genre = genres.Single(c => c.Id == 1)  },
                                       new Movie {  Name = "Tarzan", DirectorName = "Spike Lee", ReleaseDate = DateTime.Now.AddDays(-10),ArrivalDate = DateTime.Now.AddDays(-20), Created = DateTime.Now, CreatedBy = "seed", NumberInStock = 5, GenreId=2, Genre = genres.Single(c => c.Id == 2) },
                                       new Movie { Name = "Caliman", DirectorName = "Steven Spielberg", ReleaseDate = DateTime.Now.AddDays(-5),ArrivalDate = DateTime.Now.AddDays(-20), Created = DateTime.Now, CreatedBy = "seed", NumberInStock = 5, GenreId=3, Genre = genres.Single(c => c.Id == 3) },
                                       new Movie { Name = "Independence day", DirectorName = "James Cameron", ReleaseDate = DateTime.Now.AddDays(-1),ArrivalDate = DateTime.Now.AddDays(-20), Created = DateTime.Now, CreatedBy = "seed", NumberInStock = 5, GenreId=4, Genre = genres.Single(c => c.Id == 4) }
                                    };

                        movies.ForEach(m => context.Movies.AddOrUpdate(e => e.Name, m));
                        //context.Movies.AddOrUpdate(movies.ToArray());
                        context.SaveChanges();
                    }

                    var memberships = new List<MembershipType>()
                                          {
                                            new MembershipType {   SignUpFee = 0, DurationInMonths = 0, DiscountRate = 0, Created = DateTime.Now, CreatedBy = "seed" , Name = "Free" },
                                            new MembershipType {SignUpFee = 30, DurationInMonths = 1, DiscountRate = 10, Created = DateTime.Now, CreatedBy = "seed" , Name = "One Month" },
                                            new MembershipType {  SignUpFee = 90, DurationInMonths = 3, DiscountRate = 15, Created = DateTime.Now, CreatedBy = "seed", Name = "Three Month"  },
                                            new MembershipType {  SignUpFee = 300, DurationInMonths = 12, DiscountRate = 20, Created = DateTime.Now, CreatedBy = "seed", Name = "Twelve Month"  }
                                          };

                    if (!context.MembershipTypes.Any())
                    {
                        memberships.ForEach(m => context.MembershipTypes.AddOrUpdate(e => e.Name, m));
                        context.SaveChanges();
                    }



                    var customers = Customer.Create(memberships.ToArray());
                    if (!context.Customers.Any())
                    {                      
                        //context.Customers.AddOrUpdate(customers.ToArray());
                        //context.SaveChanges();
                        customers.ForEach(c => context.Customers.AddOrUpdate(e => e.Name, c));
                        context.SaveChanges();
                    }


                    if (!context.Rentals.Any())
                    {
                        context.Rentals.AddOrUpdate(T => T.TimeStamp,
                               new Rental { Customer = customers.Single(c => c.Id == 1), RentDate = Convert.ToDateTime("01/01/1984"), ReturnDate = Convert.ToDateTime("01/01/2016"), Movie = movies.Single(m => m.Id == 1), Created = DateTime.Now.AddHours(-1), TimeStamp = DateTime.Now.AddHours(-1).ToString(), CreatedBy = "seed" }
                             , new Rental { Customer = customers.Single(c => c.Id == 2), RentDate = Convert.ToDateTime("01/01/1984"), ReturnDate = Convert.ToDateTime("01/01/2016"), Movie = movies.Single(m => m.Id == 2), TimeStamp = DateTime.Now.ToString(), Created = DateTime.Now, CreatedBy = "seed" }

                             );
                        context.SaveChanges();
                    }
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