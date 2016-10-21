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
        private readonly PersistanceDBContext _context;

        public UnitOfWork(PersistanceDBContext argContext)
        {
            _context = argContext;
            //here i could initializes all repositories with the same context.
            Eventos = new EventoRepository(_context);
            Customers = new CustomerRepository(_context);
            Movies = new MovieRepository(_context);
            Genres = new GenreRepository(_context);
            Rentals = new RentalsRepository(_context);

        }
        public IEventoRepository Eventos { get; private set; }
        public ICustomerRepository Customers { get; private set; }
        public IMovieRepository Movies { get; private set; }

        public IGenreRepository Genres { get; private set; }

        public IRentalsRepository Rentals { get; private set; }

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
