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
    public class RentalsRepository : DataRepository<Rental>, IRentalsRepository
    { 
        public RentalsRepository(PersistanceDBContext context) : base(context)
        {
        }
         

        public PersistanceDBContext MyContext {
            get {
                return _Context as PersistanceDBContext;
            }
        }

        public Rental RentMovie(string CustomerId, int[] MovieIds)
        {
            throw new NotImplementedException();
        }
    }
}
