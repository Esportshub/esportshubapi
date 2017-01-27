using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Teams
 {
    public interface ITeamRepository
    {
        Task<IEnumerable<Team>> FindByAsync(Expression<Func<Team, bool>> filter, string includeProperties);
        Task<Team> FindAsync(int id);
        Task<bool> SaveAsync();
        IEnumerable<Team>  FindBy(Expression<Func<Team, bool>> filter, string includeProperties);
        Team Find(int id);
        void Insert(Team entity);
        void Delete(int id);
        void Update(Team entity);
        bool Save();
    }
}