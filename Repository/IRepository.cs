using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Repository
{
    public interface IRepository : IDisposable
    {
        //crear como parametro la entidad
        TEntity Create<TEntity>(TEntity newEntity) where TEntity : class;
        //actualizar entidad
        bool Update<TEntity>(TEntity modifiedEntity) where TEntity : class;
        //eliminar entidad
        bool Delete<TEntity>(TEntity deleteEntity) where TEntity : class;
        //buscar una entidad
        TEntity FindEntity<TEntity>
            (Expression<Func<TEntity, bool>> criteria) where TEntity : class;
        //buscar varias entidades
        IEnumerable<TEntity> FindEntitySet<TEntity>
            (Expression<Func<TEntity, bool>> criteria) where TEntity : class;
    }
}