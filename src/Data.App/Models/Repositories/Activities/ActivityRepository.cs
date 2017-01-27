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
        public ActivityRepository(EsportshubContext context, IRepository<Activity> internalRepository)
        {
            _esportshubContext = context;
            _internalRepository = internalRepository;
        }

        public async Task<IEnumerable<Activity>> FindByAsync(Expression<Func<Activity, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public async Task<Activity> FindAsync(int id)
        {
            return await _internalRepository.FindAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _internalRepository.SaveAsync();
        }

        public IEnumerable<Activity> FindBy(Expression<Func<Activity, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public Activity Find(int id)
        {
            return _internalRepository.Find(id);
        }

        public void Insert(Activity entity)
        {
            _internalRepository.Insert(entity);
        }

        public void Delete(int id)
        {
            _internalRepository.Delete(id);
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