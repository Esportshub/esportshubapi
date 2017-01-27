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
        public IntegrationRepository(EsportshubContext context, IRepository<Integration> internalRepository)
        {
            _esportshubContext = context;
            _internalRepository = internalRepository;
            _internalRepository.SetEsportshubContext(context);
        }

        public async Task<IEnumerable<Integration>> FindByAsync(Expression<Func<Integration, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public async Task<Integration> FindAsync(int id)
        {
            return await _internalRepository.FindAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _internalRepository.SaveAsync();
        }

        public IEnumerable<Integration> FindBy(Expression<Func<Integration, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public Integration Find(int id)
        {
            return _internalRepository.Find(id);
        }

        public void Insert(Integration entity)
        {
            _internalRepository.Insert(entity);
        }

        public void Delete(int id)
        {
            _internalRepository.Delete(id);
        }

        public void Update(Integration entity)
        {
            _internalRepository.Update(entity);
        }

        public bool Save()
        {
            return _internalRepository.Save();
        }
    }
}