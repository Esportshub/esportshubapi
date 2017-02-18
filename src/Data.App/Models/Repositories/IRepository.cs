using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Data.App.Models.Repositories
{
    public interface IRepository<TEntity>
    {
        EsportshubContext Context { get; set; }
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> filter, string includeProperties);
        Task<TEntity> FindAsync(Guid guid);
        Task<bool> SaveAsync();
        void SetEsportshubContext(EsportshubContext esportshubContext);

        IEnumerable<TEntity>  FindBy(Expression<Func<TEntity, bool>> filter, string includeProperties);
        TEntity Find(Guid guid);
        void Insert(TEntity entity);
        Task DeleteAsync(Guid guid);
        void Delete(Guid guid);
        void Update(TEntity entity);
        bool Save();
    }
}