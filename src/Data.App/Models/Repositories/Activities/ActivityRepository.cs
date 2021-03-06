using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Activities
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly IRepository<Activity> _internalRepository;
        private readonly EsportshubContext _esportshubContext;
        public ActivityRepository(IRepository<Activity> internalRepository)
        {

            _internalRepository = internalRepository;
            _esportshubContext = internalRepository.Context;
        }

        public async Task<IEnumerable<Activity>> FindByAsync(Expression<Func<Activity, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public async Task<Activity> FindAsync(Guid guid)
        {
            if(guid == Guid.Empty) throw new ArgumentException();
            return await _internalRepository.FindAsync(guid);
        }

        public async Task<bool> SaveAsync()
        {
            return await _internalRepository.SaveAsync();
        }

        public IEnumerable<Activity> FindBy(Expression<Func<Activity, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public Activity Find(Guid guid)
        {
            if(guid == Guid.Empty) throw new ArgumentException();
            return _internalRepository.Find(guid);
        }

        public void Insert(Activity entity)
        {
            _internalRepository.Insert(entity);
        }

        public void Delete(Guid guid)
        {
            if(guid == Guid.Empty) throw new ArgumentException();
            _internalRepository.Delete(guid);
        }

        public void Update(Activity entity)
        {
            _internalRepository.Update(entity);
        }

        public bool Save()
        {
            return _internalRepository.Save();
        }
    }
}