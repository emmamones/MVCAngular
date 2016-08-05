using Persistance.DataAccess;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public interface IEventoRepository:IRepository<Evento>
    {
        IEnumerable<Evento> GetlastEvents(int count);

        IEnumerable<Evento> GetAllEventsBy(string argUser);

    }
}
