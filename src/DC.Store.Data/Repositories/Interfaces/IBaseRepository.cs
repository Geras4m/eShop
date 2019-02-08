using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DC.Store.Data.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> : IDisposable
        where TEntity : class
    {
        Task<TEntity> SelectAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IList<TEntity>> SelectAllAsync();
        Task<IList<TEntity>> SelectAllAsync(string include);

        bool Any(Expression<Func<TEntity, bool>> predicate);

        TEntity Insert(TEntity entity);
        void Update(TEntity entity);
        Task DeleteAsync(params object[] keyValues);
        void Delete(TEntity entity);
        Task SaveChangesAsync();
    }
}
