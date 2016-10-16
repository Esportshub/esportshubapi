
using System.Collections.Generic;

namespace Models.Repositories {
    public interface IRepository<TEntity> {
            IEnumerable<TEntity> Get();
            TEntity GetByID(object id);
             void Insert(TEntity entity);
             void Delete(object id);
             void Delete(TEntity entityToDelete);
             void Update(TEntity entityToUpdate);
    }
}