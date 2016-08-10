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
        public MovieRepository(PersistanceContext context) : base(context)
        {
        }
         
        public IEnumerable<Movie> GetlastMovies(int count)
        {
           return MyContext.Movies.OrderByDescending(c => c.ReleaseDate).Take(count).ToList();
        }

        public IEnumerable<Movie> GetAllMoviesBy(string argDirectorName)
        {
            return MyContext.Movies.Where(m => m.DirectorName.Equals(argDirectorName)).ToList();
        }

        public IEnumerable<Movie> GetAllMoviesWithGender(int count)
        {
            return MyContext.Movies.Include(m=>m.Genre).Take(count).ToList();
        }

        public PersistanceContext MyContext {
            get {
                return _Context as PersistanceContext;
            }
        }
    }
}
