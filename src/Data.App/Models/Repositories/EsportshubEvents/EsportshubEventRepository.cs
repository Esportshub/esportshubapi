using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.EsportshubEvents
{
    public class EsportshubEventRepository : IEsportshubEventRepository
    {
        private readonly IRepository<EsportshubEvent> _internalRepository;
        private readonly EsportshubContext _esportshubContext;
        public EsportshubEventRepository(EsportshubContext context, IRepository<EsportshubEvent> internalRepository)
        {
            _esportshubContext = context;
            _internalRepository = internalRepository;
            _internalRepository.SetEsportshubContext(context);
        }

        public async Task<IEnumerable<EsportshubEvent>> FindByAsync(Expression<Func<EsportshubEvent, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public async Task<EsportshubEvent> FindAsync(Guid guid)
        {
            return await _internalRepository.FindAsync(guid);
        }

        public async Task<bool> SaveAsync()
        {
            return await _internalRepository.SaveAsync();
        }

        public IEnumerable<EsportshubEvent> FindBy(Expression<Func<EsportshubEvent, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public EsportshubEvent Find(Guid guid)
        {
            return _internalRepository.Find(guid);
        }

        public void Insert(EsportshubEvent entity)
        {
            _internalRepository.Insert(entity);
        }

        public void Delete(Guid guid)
        {
            _internalRepository.Delete(guid);
        }

        public void Update(EsportshubEvent entity)
        {
            _internalRepository.Update(entity);
        }

        public bool Save()
        {
            return _internalRepository.Save();
        }
    }
}