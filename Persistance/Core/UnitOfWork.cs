using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistance.Repositories;
using DataModel.Repositories;

namespace Persistance.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PersistanceContext _context;

         public UnitOfWork(PersistanceContext argContext)
        {
            _context = argContext;
            //here i could initializes all repositories with the same context.
            Eventos = new EventoRepository(_context);
            Movies = new MovieRepository(_context);
            Genres = new GenreRepository(_context);

        }
        public IEventoRepository Eventos  {     get;     private set; }
        public IMovieRepository Movies { get; private set; }

        public IGenreRepository Genres { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
