using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Players
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly IRepository<Player> _internalRepository;
        private readonly EsportshubContext _esportshubContext;

        public PlayerRepository(IRepository<Player> internalRepository)
        {
            _internalRepository = internalRepository;
            _esportshubContext = internalRepository.Context;
        }

        public virtual async Task<IEnumerable<Player>> FindByAsync(Expression<Func<Player, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public virtual async Task<Player> FindAsync(Guid guid)
        {
            if (guid == Guid.Empty) throw new ArgumentException();
            return await _internalRepository.FindAsync(guid);
        }

        public virtual async Task<bool> SaveAsync()
        {

            var result = _internalRepository.SaveAsync();
            return await result;
        }

        public virtual IEnumerable<Player> FindBy(Expression<Func<Player, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public virtual Player Find(Guid guid)
        {
            if (guid == Guid.Empty) throw new ArgumentException();
            return _internalRepository.Find(guid);
        }

        public virtual void Insert(Player entity)
        {
            _internalRepository.Insert(entity);
        }

        public virtual void Delete(Guid guid)
        {
            _internalRepository.Delete(guid);
        }

        public virtual Task DeleteAsync(Guid guid)
        {
            return _internalRepository.DeleteAsync(guid);
        }

        public virtual void Update(Player entity)
        {
            _internalRepository.Update(entity);
        }

        public virtual bool Save()
        {
            return _internalRepository.Save();
        }
    }
}