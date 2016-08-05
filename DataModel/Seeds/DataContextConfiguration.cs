using Persistance.DataModel;
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
            }
        }
    }
}