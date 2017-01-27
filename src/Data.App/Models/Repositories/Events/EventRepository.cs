using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities.Events;

namespace Data.App.Models.Repositories.Events
{
    public class EventRepository : IEventRepository
    {
        private readonly IRepository<Event> _internalRepository;
        private readonly EsportshubContext _esportshubContext;
        public EventRepository(EsportshubContext context, IRepository<Event> internalRepository)
        {
            _esportshubContext = context;
            _internalRepository = internalRepository;
        }

        public async Task<IEnumerable<Event>> FindByAsync(Expression<Func<Event, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public async Task<Event> FindAsync(int id)
        {
            return await _internalRepository.FindAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _internalRepository.SaveAsync();
        }

        public IEnumerable<Event> FindBy(Expression<Func<Event, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public Event Find(int id)
        {
            return _internalRepository.Find(id);
        }

        public void Insert(Event entity)
        {
            _internalRepository.Insert(entity);
        }

        public void Delete(int id)
        {
            _internalRepository.Delete(id);
        }

        public void Update(Event entity)
        {
            _internalRepository.Update(entity);
        }

        public bool Save()
        {
            return _internalRepository.Save();
        }
    }
}