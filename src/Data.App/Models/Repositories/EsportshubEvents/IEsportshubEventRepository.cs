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
        Task<EsportshubEvent> FindAsync(int id);
        Task<bool> SaveAsync();
        IEnumerable<EsportshubEvent>  FindBy(Expression<Func<EsportshubEvent, bool>> filter, string includeProperties);
        EsportshubEvent Find(int id);
        void Insert(EsportshubEvent entity);
        void Delete(int id);
        void Update(EsportshubEvent entity);
        bool Save();
    }
}