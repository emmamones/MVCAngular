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
        public EventoRepository(PersistanceContext context) : base(context)
        {
        }
        public IEnumerable<Evento> GetlastEvents(int count)
        {           
          return MyContext.Evento.OrderByDescending(c => c.DateEvent).Take(count).ToList();
        }

        public IEnumerable<Evento> GetAllEventsBy(string argUser)
        {
            return MyContext.Evento.Where(c => c.ApplicationUser.UserName.Equals(argUser)).ToList();
        }

        public PersistanceContext MyContext {
            get {
                return _Context as PersistanceContext;
            }
        }
    }
}
