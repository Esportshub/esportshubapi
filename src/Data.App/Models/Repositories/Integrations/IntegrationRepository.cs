using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Integrations
{
    public class IntegrationRepository : IIntegrationRepository
    {
        private readonly IRepository<Integration> _internalRepository;
        private readonly EsportshubContext _esportshubContext;
        public IntegrationRepository(IRepository<Integration> internalRepository)
        {
            _internalRepository = internalRepository;
            _esportshubContext = internalRepository.Context;
        }

        public virtual async Task<IEnumerable<Integration>> FindByAsync(Expression<Func<Integration, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public virtual async Task<Integration> FindAsync(Guid guid)
        {
            if(guid == Guid.Empty) throw new ArgumentException();
            return await _internalRepository.FindAsync(guid);
        }

        public virtual async Task<bool> SaveAsync()
        {
            return await _internalRepository.SaveAsync();
        }

        public virtual IEnumerable<Integration> FindBy(Expression<Func<Integration, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public virtual Integration Find(Guid guid)
        {
            if(guid == Guid.Empty) throw new ArgumentException();
            return _internalRepository.Find(guid);
        }

        public virtual void Insert(Integration entity)
        {
            _internalRepository.Insert(entity);
        }

        public virtual void Delete(Guid guid)
        {
            if(guid == Guid.Empty) throw new ArgumentException();
            _internalRepository.Delete(guid);
        }

        public virtual void Update(Integration entity)
        {
            _internalRepository.Update(entity);
        }

        public virtual bool Save()
        {
            return _internalRepository.Save();
        }
    }
}