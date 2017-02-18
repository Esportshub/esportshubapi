using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Games
 {
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> FindByAsync(Expression<Func<Game, bool>> filter, string includeProperties);
        Task<Game> FindAsync(Guid guid);
        Task<bool> SaveAsync();
        IEnumerable<Game>  FindBy(Expression<Func<Game, bool>> filter, string includeProperties);
        Game Find(Guid guid);
        void Insert(Game entity);
        void Delete(Guid guid);
        void Update(Game entity);
        bool Save();
    }
}