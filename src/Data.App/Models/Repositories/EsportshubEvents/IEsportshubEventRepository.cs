using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.EsportshubEvents
 {
    public interface IEsportshubEventRepository
    {
        Task<IEnumerable<EsportshubEvent>> FindByAsync(Expression<Func<EsportshubEvent, bool>> filter, string includeProperties);
        Task<EsportshubEvent> FindAsync(Guid guid);
        Task<bool> SaveAsync();
        IEnumerable<EsportshubEvent>  FindBy(Expression<Func<EsportshubEvent, bool>> filter, string includeProperties);
        EsportshubEvent Find(Guid guid);
        void Insert(EsportshubEvent entity);
        void Delete(Guid guid);
        void Update(EsportshubEvent entity);
        bool Save();
    }
}