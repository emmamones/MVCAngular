
using Persistance.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace Persistance.DataAccess
{
    public class DataRepository<T> : IRepository<T> where T : class
    {
        protected DbContext _Context;
        public Database Database
        {
            get
            {
                return _Context.Database;
            }
        }

        public DataRepository(DbContext argContext)
        {
            _Context = argContext;
        }

        public DbContext GetContext()
        {
            return _Context;
        }

        public DataRepository(string argDBConnectionString)
        {
            var sBuilder = new SqlConnectionStringBuilder(argDBConnectionString);
            sBuilder.MultipleActiveResultSets = true;

            _Context = new PersistanceDBContext(sBuilder.ToString());
        }


        public string GetTableName()
        {
            string result = null;
            var objectContext = ((IObjectContextAdapter)_Context).ObjectContext;
            string sql = objectContext.CreateObjectSet<T>().ToTraceString();

            var match = Regex.Match(sql, @"FROM\s+\[dbo\]\.\[(?<TableName>[^\]]+)\]", RegexOptions.Multiline);
            if (match.Success)
            {
                result = match.Groups["TableName"].Value;
            }

            return result;

        }

        public virtual IQueryable Read(Type argEntityType)
        {
            return _Context.Set(argEntityType);
        }
         
        public T Get(int argId)
        {
            return _Context.Set<T>().Find(argId);
        }

        public  IEnumerable<T> GetAll()
        {
            return _Context.Set<T>().ToList();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _Context.Set<T>().Where(predicate);
        }

        public void Add(T entity, string userName) 
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            //entity.Created = DateTime.Now;
            //entity.CreatedBy = userName;

            _Context.Set<T>().Add(entity);
        }


        public void Remove(T entity) 
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _Context.Set<T>().Remove(entity);
        }


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _Disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_Disposed)
            {
                if (disposing)
                {
                    // Dispose managed resources.
                    _Context.Dispose();
                }

                // Dispose unmanaged resources (nothing to do).

                _Disposed = true;

            }
        }

        ~DataRepository()
        {
            Dispose(false);
        }


        public static IDictionary<string, string> GetPropertyValueMismatches(object serverObject, object clientObject, string propertyPath)
        {
            var result = new Dictionary<string, string>();
            if (serverObject == null)
                throw new ArgumentNullException("serverObject");
            if (clientObject == null)
                throw new ArgumentNullException("clientObject");

            foreach (var prop in serverObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                // Skip properties that are not mapped to database
                //if (prop.GetCustomAttributes(true).OfType<NotMappedAttribute>().Count() > 0)
                //    continue;

                var serverValue = prop.GetValue(serverObject, null);
                var clientValue = clientObject.GetType().GetProperty(prop.Name).GetValue(clientObject, null);

                var mismatch = false;

                if (serverValue != null)
                    mismatch = !serverValue.Equals(clientValue);
                else if (clientValue != null)
                    mismatch = !clientValue.Equals(serverValue);

                if (mismatch)
                {
                    //if (prop.PropertyType.IsCustomClass())
                    //{
                    //    propertyPath += prop.Name + ".";
                    //    GetPropertyValueMismatches(controller, serverValue, clientValue, propertyPath);
                    //}
                    result.Add(propertyPath + prop.Name, "Database value: '" + serverValue + "'");
                }
            }
            return result;
        }



        public IEnumerable<T> GetFilteredElements<S>(Expression<Func<T, bool>> argFilter, int argPageIndex, int argPageCount, Expression<Func<T, S>> argOrderByExpression, bool argAscending) 
        {
            //checking query arguments
            if (argFilter == (Expression<Func<T, bool>>)null)
                throw new ArgumentNullException("filter");

            if (argPageIndex < 0)
                throw new ArgumentException("pageIndex");

            if (argPageCount <= 0)
                throw new ArgumentException("pageCount");

            if (argOrderByExpression == (Expression<Func<T, S>>)null)
                throw new ArgumentNullException("orderByExpression");


            var objectSet = _Context.Set<T>();

            return (argAscending)
                                ?
                                    objectSet
                                     .Where(argFilter)
                                     .OrderBy(argOrderByExpression)
                                     .Skip(argPageIndex * argPageCount)
                                     .Take(argPageCount)
                                     .ToList()
                                :
                                    objectSet
                                     .Where(argFilter)
                                     .OrderByDescending(argOrderByExpression)
                                     .Skip(argPageIndex * argPageCount)
                                     .Take(argPageCount)
                                     .ToList();
        }

      
    }
}