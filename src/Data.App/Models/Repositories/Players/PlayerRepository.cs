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

        public PlayerRepository(EsportshubContext context, IRepository<Player> internalRepository)
        {
            _esportshubContext = context;
            _internalRepository = internalRepository;
            _internalRepository.SetEsportshubContext(context);
        }

        public virtual async Task<IEnumerable<Player>> FindByAsync(Expression<Func<Player, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public virtual async Task<Player> FindAsync(int id)
        {
            return await _internalRepository.FindAsync(id);
        }

        public virtual async Task<bool> SaveAsync()
        {
            return await _internalRepository.SaveAsync();
        }

        public virtual IEnumerable<Player> FindBy(Expression<Func<Player, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public virtual Player Find(int id)
        {
            return _internalRepository.Find(id);
        }

        public virtual void Insert(Player entity)
        {
            _internalRepository.Insert(entity);
        }

        public virtual void Delete(int id)
        {
            _internalRepository.Delete(id);
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