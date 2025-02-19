using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoLuisOrellana.Infrastructure.Repository.Base
{
    public interface IRepositoryBase<TEntity>
    {
        Task<TEntity> GetById(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
        Task<IEnumerable<TEntity>> List(IEnumerable<string> includes = null);
        Task<IEnumerable<TEntity>> List(Expression<Func<TEntity, bool>> predicate, IEnumerable<string> includes = null);
        Task Insert(TEntity entity);
        Task Delete(TEntity entity);
        Task Update(TEntity entity);
        Task ExecuteSerializableInsert(TEntity entity);

    }
}
