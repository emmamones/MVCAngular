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
    public class MovieRepository : DataRepository<Movie>, IMovieRepository
    { 
        public MovieRepository(PersistanceDBContext context) : base(context)
        {
        }
         
        public IEnumerable<Movie> GetlastMovies(int count)
        {
           return MyContext.Movies.OrderByDescending(c => c.ReleaseDate).Take(count).ToList();
        }

        public IEnumerable<Movie> ByGenre(int id)
        {
            return MyContext.Movies.Include(m => m.Genre).Where(m => m.GenreId.Equals(id)).ToList();
        }

        public IEnumerable<Movie> GetAllMoviesWithGender(int count)
        {
            try
            {

              return  MyContext.Movies.Include(m => m.Genre).Take(count).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        
        }

        public PersistanceDBContext MyContext {
            get {
                return _Context as PersistanceDBContext;
            }
        }
    }
}
