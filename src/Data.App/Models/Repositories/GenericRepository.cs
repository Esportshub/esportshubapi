using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.App.Models.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEsportshubEntity
    {
        private EsportshubContext _context;
        private DbSet<TEntity> _dbSet;

        public GenericRepository()
        {
        }

        public void SetEsportshubContext(EsportshubContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        //@TODO: Should be handled explicit
        public async Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> filter, string includeProperties)
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);
            return await query.ToListAsync();
        }

        public async Task<TEntity> FindAsync(Guid guid) => await _dbSet.SingleAsync(x => x.Guid.Equals(guid));

        public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() >= 0;

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
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async void Delete(Guid guid)
        {
            var entityToDelete = await FindAsync(guid);
            Delete(entityToDelete);
        }

        public bool Save() => _context.SaveChanges() >= 0;


        public void OnCompleted(Action continuation)
        {
            throw new NotImplementedException();
        }

        private void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
                _dbSet.Attach(entityToDelete);
            _dbSet.Remove(entityToDelete);
        }
    }
}