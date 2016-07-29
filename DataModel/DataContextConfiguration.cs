using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace DataModel
{
    /// <summary>
    /// sealed does not allow to inherit from it.
    /// </summary>
    public sealed class DataContextConfiguration : DbMigrationsConfiguration<ModelContext>
    {
        public DataContextConfiguration()
        {

            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
        MyUserInfo AddUserAndRole(ModelContext context)
        {
            var user = new MyUserInfo()
            {
                UserName = "user1@contoso.com",
                FirstName = "Emmanuel",
                LastName = "Lohora",
                Eventos = new System.Collections.Generic.List<EventoExterno>()
            };

            return user;
        }


        protected override void Seed(ModelContext context)
        {

            using (DbContextTransaction dbContextTransaction = context.Database.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {

                var defaultUser = AddUserAndRole(context);

                context.EventoExterno.AddOrUpdate(p => p.Title,
                      new EventoExterno
                      {
                          Title = "Debra Garcia",
                          ApplicationUser = defaultUser
                      },
                      new EventoExterno
                      {
                          Title = "Thorsten Weinrich",
                          ApplicationUser = defaultUser
                      },
                      new EventoExterno
                      {
                          Title = "Yuhong Li",
                          ApplicationUser = defaultUser
                      },
                      new EventoExterno
                      {
                          Title = "Jon Orton",
                          ApplicationUser = defaultUser
                      },
                      new EventoExterno
                      {
                          Title = "Diliana Alexieva-Bosseva",
                          ApplicationUser = defaultUser
                      }
                      );
            }
        }
    }
}