using Persistance.DataModel;
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Persistance.Seeds
{
    /// <summary>
    /// sealed does not allow to inherit from it.
    /// </summary>
    public sealed class DataContextConfiguration : DropCreateDatabaseAlways<PersistanceContext>
    {
        public DataContextConfiguration()
        {
             
        }
        MyUserInfo AddUserAndRole(PersistanceContext context)
        {
            var user = new MyUserInfo()
            {
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

                context.Movie.AddOrUpdate(m => m.Name,
                    new Movie { Name="Shrek!", DirectorName="Spike Lee" , ReleaseDate=DateTime.Now.AddDays(-15)},
                    new Movie { Name="Tarzan", DirectorName = "Spike Lee" , ReleaseDate = DateTime.Now.AddDays(-10) },
                    new Movie { Name="Caliman", DirectorName = "Steven Spielberg", ReleaseDate = DateTime.Now.AddDays(-5) },
                    new Movie { Name="Independence day", DirectorName = "James Cameron", ReleaseDate = DateTime.Now.AddDays(-1) });
            }
        }
    }
}