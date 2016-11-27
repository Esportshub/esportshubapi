using System;
using System.Collections.Generic;
using RestfulApi.App.Models.Esportshub;
using RestfulApi.App.Models.Esportshub.Entities;
using RestfulApi.App.Models.Repositories;

namespace RestfulApi.App.UnitOfWork
{
    public class UnitOfWork: IDisposable
    {
        private readonly EsportshubContext _dbContext;
        private bool _disposed;
        private Dictionary<string, object> _repositories;

        public UnitOfWork(EsportshubContext dbContext)
        {
            _dbContext = dbContext;
        }

        public UnitOfWork()
        {
            _dbContext = new EsportshubContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if(!_disposed)
                if(disposing)
                _dbContext.Dispose();

            _disposed = true;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : IEsportshubEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<string,object>();
            }

            var type = typeof(TEntity).Name;

            if (_repositories.ContainsKey(type)) return (IRepository<TEntity>) _repositories[type];

            var repositoryType = typeof(IRepository<TEntity>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);
            _repositories.Add(type, repositoryInstance);
            return (IRepository<TEntity>)_repositories[type];
        }

    }
}