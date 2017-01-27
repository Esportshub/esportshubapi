
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Games
{
    public class GameRepository : IGameRepository
    {
        private readonly IRepository<Game> _internalRepository;
        private readonly EsportshubContext _esportshubContext;
        public GameRepository(EsportshubContext context, IRepository<Game> internalRepository)
        {
            _esportshubContext = context;
            _internalRepository = internalRepository;
        }

        public async Task<IEnumerable<Game>> FindByAsync(Expression<Func<Game, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public async Task<Game> FindAsync(int id)
        {
            return await _internalRepository.FindAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _internalRepository.SaveAsync();
        }

        public IEnumerable<Game> FindBy(Expression<Func<Game, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public Game Find(int id)
        {
            return _internalRepository.Find(id);
        }

        public void Insert(Game entity)
        {
            _internalRepository.Insert(entity);
        }

        public void Delete(int id)
        {
            _internalRepository.Delete(id);
        }

        public void Update(Game entity)
        {
            _internalRepository.Update(entity);
        }

        public bool Save()
        {
            return _internalRepository.Save();
        }
    }
}