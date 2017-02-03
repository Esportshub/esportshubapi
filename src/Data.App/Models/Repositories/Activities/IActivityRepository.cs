using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Activities
 {
    public interface IActivityRepository
    {
        Task<IEnumerable<Activity>> FindByAsync(Expression<Func<Activity, bool>> filter, string includeProperties);
        Task<Activity> FindAsync(Guid guid);
        Task<bool> SaveAsync();
        IEnumerable<Activity>  FindBy(Expression<Func<Activity, bool>> filter, string includeProperties);
        Activity Find(Guid guid);
        void Insert(Activity entity);
        void Delete(Guid guid);
        void Update(Activity entity);
        bool Save();
    }
}