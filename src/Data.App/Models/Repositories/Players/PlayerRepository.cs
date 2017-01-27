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

        public async Task<IEnumerable<Player>> FindByAsync(Expression<Func<Player, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public async Task<Player> FindAsync(int id)
        {
            return await _internalRepository.FindAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _internalRepository.SaveAsync();
        }

        public IEnumerable<Player> FindBy(Expression<Func<Player, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public Player Find(int id)
        {
            return _internalRepository.Find(id);
        }

        public void Insert(Player entity)
        {
            _internalRepository.Insert(entity);
        }

        public void Delete(int id)
        {
            _internalRepository.Delete(id);
        }

        public void Update(Player entity)
        {
            _internalRepository.Update(entity);
        }

        public bool Save()
        {
            return _internalRepository.Save();
        }
    }
}