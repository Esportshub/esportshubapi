using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Players
 {
    public interface IPlayerRepository
    {
        Task<IEnumerable<Player>> FindByAsync(Expression<Func<Player, bool>> filter, string includeProperties);
        Task<Player> FindAsync(Guid guid);
        Task<bool> SaveAsync();
        IEnumerable<Player>  FindBy(Expression<Func<Player, bool>> filter, string includeProperties);
        Player Find(Guid guid);
        void Insert(Player entity);
        void Delete(Guid guid);
        Task DeleteAsync(Guid guid);
        void Update(Player entity);
        bool Save();
    }
}