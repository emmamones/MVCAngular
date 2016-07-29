using AngularMVCAuthentication.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AngularMVCAuthentication.DataAccess
{
    public interface IDataRepository : IDisposable
    {
        IQueryable Read(Type argEntityType);
        IQueryable<T> Read<T>() where T : CModelBase;
        T Find<T>(int id) where T : CModelBase;
        void Create<T>(T entity, string userName) where T : CUserEntity;
        void Update<T>(T entity, string userName) where T : CUserEntity;
        void Delete<T>(T entity) where T : CModelBase;

        void Remove<T>(T entity) where T : CModelBase;
        void SaveChanges();
        /// <summary>
        /// Get all elements of type {T} in repository
        /// </summary>
        /// <param name="filter">Filter that each element do match</param>
        /// <param name="pageIndex">Index of page</param>
        /// <param name="pageCount">Number of elements in each page</param>
        /// <param name="orderByExpression">Order by expression</param>
        /// <param name="argAscending">Specify if order is ascending</param>
        /// <returns>List of selected elements</returns>
        IEnumerable<T> GetFilteredElements<T, S>(Expression<Func<T, bool>> argFilter, int argPageIndex, int argPageCount, Expression<Func<T, S>> argOrderByExpression, bool argAscending) where T : CModelBase;

    }
}
