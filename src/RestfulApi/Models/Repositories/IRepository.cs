using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EsportshubApi.Models.Repositories
{
    public interface IRepository<TEntity> {
            IEnumerable<TEntity> Get( Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "");
             TEntity GetById(object id);
             void Insert(TEntity entity);
             void Delete(object id);
             void Update(TEntity entityToUpdate);
    }
}