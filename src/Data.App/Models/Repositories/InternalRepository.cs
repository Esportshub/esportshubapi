using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.App.Models.Repositories
{
    public class InternalRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEsportshubEntity
    {
        public EsportshubContext Context { get; set; }
        private DbSet<TEntity> _dbSet;

        public InternalRepository()
        {
        }

        public InternalRepository(EsportshubContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }

        public void SetEsportshubContext(EsportshubContext context)
        {
            Context = context;
            _dbSet = Context.Set<TEntity>();
        }


        public async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);
            return await query.ToListAsync();
        }

        public async Task<TEntity> FindAsync(Guid guid) => await _dbSet.SingleAsync(x => x.Guid == guid);

        public async Task<bool> SaveAsync() => await Context.SaveChangesAsync() >= 0;

        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);
            return query.ToList();
        }

        public TEntity Find(Guid guid) => _dbSet.Single(x => x.Guid == guid);

        public void Insert(TEntity entity) => _dbSet.Add(entity);


        public void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task DeleteAsync(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                throw new ArgumentException();
            }
            var entityToDelete = await FindAsync(guid);
            Delete(entityToDelete);
        }

        public void Delete(Guid guid)
        {
            if (guid == Guid.Empty)
            {
                throw new ArgumentException();
            }
            var entityToDelete = Find(guid);
            Delete(entityToDelete);
        }

        public bool Save() => Context.SaveChanges() >= 0;


        public void OnCompleted(Action continuation)
        {
            throw new NotImplementedException();
        }

        private void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);
            _dbSet.Remove(entityToDelete);
        }
    }
}