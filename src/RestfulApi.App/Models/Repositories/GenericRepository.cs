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
        private EsportshubContext _context;
        private DbSet<TEntity> _dbSet;

        protected GenericRepository(EsportshubContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);
            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(int id) => await _dbSet.SingleAsync(x => x.Id == id);

        public virtual void Insert(TEntity entity) => _dbSet.Add(entity);

        public virtual async Task DeleteAsync(int id)
        {
            var entityToDelete = await GetByIdAsync(id);
            DeleteFinale(entityToDelete);
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public virtual TEntity Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
//            await _context.SaveChangesAsync(); // Remove
            return entityToUpdate;
        }


        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            var entityToDelete = GetById(id);
            DeleteFinale(entityToDelete);
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }

        public void OnCompleted(Action continuation)
        {
            throw new NotImplementedException();
        }

        private void DeleteFinale(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);
            _dbSet.Remove(entityToDelete);
//            await _context.SaveChangesAsync();//Uncomment
        }
    }
}