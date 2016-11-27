using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestfulApi.App.Models.Esportshub;
using RestfulApi.App.Models.Esportshub.Entities;

namespace RestfulApi.App.Models.Repositories
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEsportshubEntity 
    {
        internal EsportshubContext context;
        internal DbSet<TEntity> dbSet;

        protected GenericRepository(EsportshubContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);
                
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.SingleAsync(x => x.Id == id);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
             dbSet.Add(entity);
             await context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id)
        {
            TEntity entityToDelete = await dbSet.SingleAsync(x => x.Id == id);
            await DeleteAsync(entityToDelete);
        }

         private async Task DeleteAsync(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);
            dbSet.Remove(entityToDelete);
            await context.SaveChangesAsync();
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entityToUpdate;
        }

        public void OnCompleted(Action continuation)
        {
            throw new NotImplementedException();
        }
    }
}