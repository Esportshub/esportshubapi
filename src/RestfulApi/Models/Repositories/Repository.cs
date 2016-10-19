using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EsportshubApi.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace EsportshubApi.Models.Repositories
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal EsportshubContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(EsportshubContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);
                
            return query.ToList();

        }

        public virtual TEntity GetById(object id)
        {
            return dbSet.Single(x => x.Equals(id));
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);

        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Single(x => x.Equals(id));
            Delete(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }


        private void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);

            dbSet.Remove(entityToDelete);
        }


    }
}