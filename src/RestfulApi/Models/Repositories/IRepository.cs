using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestfulApi.Models.Repositories
{
    public interface IRepository<TEntity>
    {
             Task<IEnumerable<TEntity>> GetAsync( Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
             Task<TEntity> GetByIdAsync(int id);
             Task InsertAsync(TEntity entity);
             Task DeleteAsync(int id);
             Task<TEntity> UpdateAsync(TEntity entity);
    }
}