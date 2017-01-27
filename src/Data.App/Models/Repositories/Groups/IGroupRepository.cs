using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Groups
 {
    public interface IGroupRepository
    {
        Task<IEnumerable<Group>> FindByAsync(Expression<Func<Group, bool>> filter, string includeProperties);
        Task<Group> FindAsync(int id);
        Task<bool> SaveAsync();
        IEnumerable<Group>  FindBy(Expression<Func<Group, bool>> filter, string includeProperties);
        Group Find(int id);
        void Insert(Group entity);
        void Delete(int id);
        void Update(Group entity);
        bool Save();
    }
}