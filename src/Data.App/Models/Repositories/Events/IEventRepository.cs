using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities.Events;

namespace Data.App.Models.Repositories.Events
 {
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> FindByAsync(Expression<Func<Event, bool>> filter, string includeProperties);
        Task<Event> FindAsync(int id);
        Task<bool> SaveAsync();
        IEnumerable<Event>  FindBy(Expression<Func<Event, bool>> filter, string includeProperties);
        Event Find(int id);
        void Insert(Event entity);
        void Delete(int id);
        void Update(Event entity);
        bool Save();
    }
}