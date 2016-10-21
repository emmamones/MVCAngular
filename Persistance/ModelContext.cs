 using Microsoft.AspNet.Identity.EntityFramework;
using Persistance.DataModel;

namespace Persistance
{

    public class ModelContext : IdentityDbContext<ApplicationUser>
    {
        public const string _DatabaseName = "AngularMVCJourny";

        public ModelContext(string argConnectionString)
           : base(argConnectionString, throwIfV1Schema: false)
        {

        }
        public ModelContext()
            : base(_DatabaseName, throwIfV1Schema: false)
        {

        }

        public static ModelContext Create()
        {
            return new ModelContext();
        }

    }
}
