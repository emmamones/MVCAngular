
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.DataAccess
{
    public interface IRepository<T> where T : class
    {
        DbContext GetContext();
        T Get(int argId);
        IEnumerable<T> GetAll();  
        IEnumerable<T> Find(Expression<Func<T,bool>> argPredicate);
        void Add(T entity, string userName); 

        void Remove(T entity); 
        /// <summary>
        /// Get all elements of type {T} in repository
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="orderByExpression">Order by expression</param>
        /// <param name="argAscending">Specify if order is ascending</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<T> GetFilteredElements<S>(Expression<Func<T, bool>> argFilter, int argPageIndex, int argPageCount, Expression<Func<T, S>> argOrderByExpression, bool argAscending);

    }
}
