using DataModel.Repositories;
using Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Core
{
    public interface IUnitOfWork:IDisposable
    {
        IEventoRepository Eventos { get; }

        IMovieRepository Movies { get; }
        int Complete();
    }
}
