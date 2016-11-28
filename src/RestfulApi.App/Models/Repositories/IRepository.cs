using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RestfulApi.App.Models.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        Task<TEntity> GetByIdAsync(int id);
//         InsertAsync(TEntity entity);
//        Task DeleteAsync(int id);
//        Task<TEntity> UpdateAsync(TEntity entity);
        Task<bool> SaveAsync();

        IEnumerable<TEntity>  Get(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "");
        TEntity GetById(int id);
        void Insert(TEntity entity);
        void Delete(int id);
        TEntity Update(TEntity entity);
        bool Save();
    }
}