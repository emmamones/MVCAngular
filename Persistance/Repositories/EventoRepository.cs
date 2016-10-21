using Persistance.DataAccess;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class EventoRepository : DataRepository<Evento>, IEventoRepository
    { 
        public EventoRepository(PersistanceDBContext context) : base(context)
        {
        }
        public IEnumerable<Evento> GetlastEvents(int count)
        {           
          return MyContext.Eventos.OrderByDescending(c => c.DateEvent).Take(count).ToList();
        }

        public IEnumerable<Evento> GetAllEventsBy(string argUser)
        {
            return MyContext.Eventos.Where(c => c.ApplicationUserName.Equals(argUser)).ToList();
        }

        public PersistanceDBContext MyContext {
            get {
                return _Context as PersistanceDBContext;
            }
        }
    }
}
