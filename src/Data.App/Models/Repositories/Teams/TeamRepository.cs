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

        public TeamRepository(IRepository<Team> internalRepository)
        {
            _internalRepository = internalRepository;
            _esportshubContext = internalRepository.Context;
        }

        public virtual async Task<IEnumerable<Team>> FindByAsync(Expression<Func<Team, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public virtual async Task<Team> FindAsync(Guid guid)
        {
            if(guid == Guid.Empty) throw new ArgumentException();
            return await _internalRepository.FindAsync(guid);
        }

        public virtual async Task<bool> SaveAsync()
        {
            return await _internalRepository.SaveAsync();
        }

        public virtual IEnumerable<Team> FindBy(Expression<Func<Team, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public virtual Team Find(Guid guid)
        {
            if(guid == Guid.Empty) throw new ArgumentException();
            return _internalRepository.Find(guid);
        }

        public virtual void Insert(Team entity)
        {
            _internalRepository.Insert(entity);
        }

        public virtual void Delete(Guid guid)
        {
            if(guid == Guid.Empty) throw new ArgumentException();
            _internalRepository.Delete(guid);
        }

        public virtual void Update(Team entity)
        {
            _internalRepository.Update(entity);
        }

        public virtual bool Save()
        {
            return _internalRepository.Save();
        }
    }
    
}