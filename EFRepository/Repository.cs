using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Repository;
using System.Data.Entity;

namespace EFRepository
{
    public class Repository : IRepository, IDisposable
    {
        protected DbContext Context;
        public Repository(DbContext context,
            bool autoDetectChangesEnabled = false,
            bool proxyCreationEnabled = false)
        {
            this.Context = context;
            this.Context.Configuration.AutoDetectChangesEnabled = autoDetectChangesEnabled;
            this.Context.Configuration.ProxyCreationEnabled = proxyCreationEnabled;
        }
        
        public TEntity Create<TEntity>(TEntity newEntity) where TEntity : class
        {
            TEntity Result = null;
            try
            {
                Result = Context.Set<TEntity>().Add(newEntity);
                TrySaveChange();
            }
            catch (Exception e)
            {
                throw (e);
            }
            return Result;
        }

        protected virtual int TrySaveChange()
        {
            return Context.SaveChanges();
        }

        public bool Delete<TEntity>(TEntity deleteEntity) where TEntity : class
        {
            bool Result = false;
            try
            {
                Context.Set<TEntity>().Attach(deleteEntity);
                Context.Set<TEntity>().Remove(deleteEntity);
                Result = TrySaveChange() > 0;

            }
            catch (Exception e)
            {
                throw (e);
            }
            return Result;
        }

        public void Dispose()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }

        public TEntity FindEntity<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            TEntity Result = null;
            try
            {
                Result = Context.Set<TEntity>().FirstOrDefault(criteria);
            }
            catch (Exception e)
            {
                throw (e);
            }
            return Result;
        }

        public IEnumerable<TEntity> FindEntitySet<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            List<TEntity> Result = null;
            try
            {
                Result = Context.Set<TEntity>().Where(criteria).ToList();
            }
            catch (Exception e)
            {
                throw (e);
            }
            return Result;
        }

        public bool Update<TEntity>(TEntity modifiedEntity) where TEntity : class
        {
            bool Result = false;
            try
            {
                Context.Set<TEntity>().Attach(modifiedEntity);
                Context.Entry<TEntity>(modifiedEntity).State = EntityState.Modified;
                Result = TrySaveChange() > 0;
            }
            catch (Exception e)
            {
                throw (e);
            }
            return Result;
        }
    }
}
