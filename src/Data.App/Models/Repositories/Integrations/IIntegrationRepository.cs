using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Integrations
{
    public interface IIntegrationRepository
    {
        Task<IEnumerable<Integration>> FindByAsync(Expression<Func<Integration, bool>> filter, string includeProperties);
        Task<Integration> FindAsync(Guid guid);
        Task<bool> SaveAsync();
        IEnumerable<Integration>  FindBy(Expression<Func<Integration, bool>> filter, string includeProperties);
        Integration Find(Guid guid);
        void Insert(Integration entity);
        void Delete(Guid guid);
        void Update(Integration entity);
        bool Save();
    }
}