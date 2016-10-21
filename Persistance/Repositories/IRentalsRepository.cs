using Persistance.DataAccess;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public interface IRentalsRepository:IRepository<Rental>
    {
        Rental RentMovie(string CustomerId, int[] MovieIds); 

    }
}
