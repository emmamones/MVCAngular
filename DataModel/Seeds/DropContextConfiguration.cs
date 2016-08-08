using Persistance.DataModel;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Persistance.Seeds
{
    /// <summary>
    /// sealed does not allow to inherit from it.
    /// </summary>
    public sealed class DropContextConfiguration : DropCreateDatabaseAlways<PersistanceContext>
    {
        public DropContextConfiguration()
        {

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

            using (DbContextTransaction dbContextTransaction = context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {

                var defaultUser = AddUserAndRole(context);

                context.Evento.AddOrUpdate(p => p.Title,
                      new Evento
                      { Id = 1, Title = "Debra Garcia", ApplicationUser = defaultUser, Created = DateTime.Now, CreatedBy = "seed" },
                      new Evento
                      { Id = 2, Title = "Thorsten Weinrich", ApplicationUser = defaultUser, Created = DateTime.Now, CreatedBy = "seed" },
                      new Evento
                      { Id = 3, Title = "Yuhong Li", ApplicationUser = defaultUser, Created = DateTime.Now, CreatedBy = "seed" },
                      new Evento
                      { Id = 4, Title = "Jon Orton", ApplicationUser = defaultUser, Created = DateTime.Now, CreatedBy = "seed" },
                      new Evento
                      { Id = 5, Title = "Diliana Alexieva-Bosseva", ApplicationUser = defaultUser, Created = DateTime.Now, CreatedBy = "seed" }
                      );

                context.Movie.AddOrUpdate(m => m.Name,
                    new Movie { Id = 1, Name = "Shrek!", DirectorName = "Spike Lee", ReleaseDate = DateTime.Now.AddDays(-15), Created = DateTime.Now, CreatedBy = "seed" },
                    new Movie { Id = 2, Name = "Tarzan", DirectorName = "Spike Lee", ReleaseDate = DateTime.Now.AddDays(-10), Created = DateTime.Now, CreatedBy = "seed" },
                    new Movie { Id = 3, Name = "Caliman", DirectorName = "Steven Spielberg", ReleaseDate = DateTime.Now.AddDays(-5), Created = DateTime.Now, CreatedBy = "seed" },
                    new Movie { Id = 4, Name = "Independence day", DirectorName = "James Cameron", ReleaseDate = DateTime.Now.AddDays(-1), Created = DateTime.Now, CreatedBy = "seed" });




                context.MembershipTypes.AddOrUpdate(T => T.Id,
                    new MembershipType { Id = 1, SignUpFee = 0, DurationInMonths = 0, DiscountRate = 0, Created = DateTime.Now, CreatedBy = "seed" },
                    new MembershipType { Id = 2, SignUpFee = 30, DurationInMonths = 1, DiscountRate = 10, Created = DateTime.Now, CreatedBy = "seed" },
                    new MembershipType { Id = 3, SignUpFee = 90, DurationInMonths = 3, DiscountRate = 15, Created = DateTime.Now, CreatedBy = "seed" },
                    new MembershipType { Id = 4, SignUpFee = 300, DurationInMonths = 12, DiscountRate = 20, Created = DateTime.Now, CreatedBy = "seed" });

            }
        }
    }
}