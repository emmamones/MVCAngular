using AngularMVCAuthentication.DataModel;
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

namespace AngularMVCAuthentication.DataAccess
{
    public class DataRepository : IDataRepository
    {


        public DataRepository()
        {
            _Context = new ModelContext();
        }

        public DataRepository(string argDBConnectionString)
        {
            var sBuilder = new SqlConnectionStringBuilder(argDBConnectionString);
            sBuilder.MultipleActiveResultSets = true;

            _Context = new ModelContext(sBuilder.ToString());
        }

        public Database Database
        {
            get
            {
                return Context.Database;
            }
        }


        private DbContext _Context;
        public DbContext Context
        {
            get { return _Context; }

        }

        public string GetTableName<T>() where T : CModelBase
        {
            string result = null;
            var objectContext = ((IObjectContextAdapter)Context).ObjectContext;
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
            return Context.Set(argEntityType);
        }

        public virtual IQueryable<T> Read<T>() where T : CModelBase
        {
            return Context.Set<T>();
        }

        public T Find<T>(int id) where T : CModelBase
        {
            return Read<T>().SingleOrDefault(e => e.Id == id);
        }

        public virtual void Create<T>(T entity, string userName) where T : CUserEntity
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.Created = DateTime.Now;
            entity.CreatedBy = userName;

            Context.Set<T>().Add(entity);
        }

        public virtual void Delete<T>(T entity) where T : CModelBase
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Context.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
        }


        public void Remove<T>(T entity) where T : CModelBase
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            Context.Set<T>().Remove(entity);
        }

        public virtual void Update<T>(T entity, string userName) where T : CUserEntity
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            entity.Updated = DateTime.Now;
            entity.UpdatedBy = userName;

            var set = Context.Set<T>();
            T attachedEntity = set.Find(entity.Id);  // You need to have access to key

            if (attachedEntity != null)
            {
                entity.Created = attachedEntity.Created;
                entity.CreatedBy = attachedEntity.CreatedBy;
                var attachedEntry = Context.Entry(attachedEntity);
                attachedEntry.CurrentValues.SetValues(entity);
            }
            else
            {
                Context.Entry(entity).State = System.Data.Entity.EntityState.Modified; // This should attach entity
            }

        }

        public virtual void SaveChanges()
        {


            Context.SaveChanges();

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
                    Context.Dispose();
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



        public IEnumerable<T> GetFilteredElements<T, S>(Expression<Func<T, bool>> argFilter, int argPageIndex, int argPageCount, Expression<Func<T, S>> argOrderByExpression, bool argAscending) where T : CModelBase
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


            var objectSet = Context.Set<T>();

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