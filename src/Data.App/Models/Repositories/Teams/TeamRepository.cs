using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Teams
{
    public class TeamRepository: ITeamRepository
    {
        private readonly IRepository<Team> _internalRepository;
        private readonly EsportshubContext _esportshubContext;

        public TeamRepository(EsportshubContext context, IRepository<Team> internalRepository)
        {
            _esportshubContext = context;
            _internalRepository = internalRepository;
            _internalRepository.SetEsportshubContext(context);
        }

        public async Task<IEnumerable<Team>> FindByAsync(Expression<Func<Team, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public async Task<Team> FindAsync(int id)
        {
            return await _internalRepository.FindAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _internalRepository.SaveAsync();
        }

        public IEnumerable<Team> FindBy(Expression<Func<Team, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public Team Find(int id)
        {
            return _internalRepository.Find(id);
        }

        public void Insert(Team entity)
        {
            _internalRepository.Insert(entity);
        }

        public void Delete(int id)
        {
            _internalRepository.Delete(id);
        }

        public void Update(Team entity)
        {
            _internalRepository.Update(entity);
        }

        public bool Save()
        {
            return _internalRepository.Save();
        }
    }
    
}