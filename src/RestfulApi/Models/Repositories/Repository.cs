using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using EsportshubApi.Models.Entities;
using System.Threading.Tasks;

namespace EsportshubApi.Models.Repositories
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, EsportshubEntity 
    {
        internal EsportshubContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(EsportshubContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public async virtual Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);
                
            return await query.ToListAsync();
        }

        public async virtual Task<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.SingleAsync(x => x.Equals(id));
        }

        public async virtual Task InsertAsync(TEntity entity)
        {
             dbSet.Add(entity);
             await context.SaveChangesAsync();
        }

        public async virtual Task DeleteAsync(int id)
        {
            TEntity entityToDelete = await dbSet.SingleAsync(x => x.Equals(id));
            await DeleteAsync(entityToDelete);
        }

         private async Task DeleteAsync(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);
            dbSet.Remove(entityToDelete);
            await context.SaveChangesAsync();
        }

        public async virtual Task<TEntity> UpdateAsync(TEntity entityToUpdate)
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