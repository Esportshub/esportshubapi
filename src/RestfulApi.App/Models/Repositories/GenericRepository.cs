using System;
using System.Linq;
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

        public async Task<TEntity> GetByIdAsync(int id) => await _dbSet.SingleAsync(x => x.Id == id);

        public async Task<bool> SaveAsync() => await _context.SaveChangesAsync() >= 0;

        public TEntity GetById(int id) => _dbSet.Single(x => x.Id == id);

        public void Insert(TEntity entity) => _dbSet.Add(entity);


        public void Update(TEntity entityToUpdate)
        {
            _dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entityToDelete = GetById(id);
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