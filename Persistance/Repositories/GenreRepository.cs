using DataModel.Repositories;
using Persistance.DataAccess;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Persistance.Repositories
{
    public class GenreRepository : DataRepository<Genre>, IGenreRepository
    { 
        public GenreRepository(PersistanceContext context) : base(context)
        {
        }
         

        public PersistanceContext MyContext {
            get {
                return _Context as PersistanceContext;
            }
        }
    }
}
