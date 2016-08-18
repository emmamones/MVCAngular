using Persistance.DataAccess;
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.Repositories
{
    public interface IMovieRepository:IRepository<Movie>
    {
        IEnumerable<Movie> GetlastMovies(int count);

        IEnumerable<Movie> ByGenre(int id);

        IEnumerable<Movie> GetAllMoviesWithGender(int count);

    }
}
