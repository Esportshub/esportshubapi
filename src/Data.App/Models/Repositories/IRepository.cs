using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.App.Models.Repositories
{
    public interface IRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> filter, string includeProperties);
        Task<TEntity> FindAsync(int id);
        Task<bool> SaveAsync();
        void SetEsportshubContext(EsportshubContext esportshubContext);

        IEnumerable<TEntity>  FindBy(Expression<Func<TEntity, bool>> filter, string includeProperties);
        TEntity Find(int id);
        void Insert(TEntity entity);
        void Delete(int id);
        void Update(TEntity entity);
        bool Save();
    }
}