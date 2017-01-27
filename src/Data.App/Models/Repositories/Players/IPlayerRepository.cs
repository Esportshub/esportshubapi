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
        Task<Player> FindAsync(int id);
        Task<bool> SaveAsync();
        IEnumerable<Player>  FindBy(Expression<Func<Player, bool>> filter, string includeProperties);
        Player Find(int id);
        void Insert(Player entity);
        void Delete(int id);
        void Update(Player entity);
        bool Save();
    }
}