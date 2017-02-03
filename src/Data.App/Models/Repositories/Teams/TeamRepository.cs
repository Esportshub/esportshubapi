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

        public virtual async Task<IEnumerable<Team>> FindByAsync(Expression<Func<Team, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public virtual async Task<Team> FindAsync(Guid guid)
        {
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
            return _internalRepository.Find(guid);
        }

        public virtual void Insert(Team entity)
        {
            _internalRepository.Insert(entity);
        }

        public virtual void Delete(Guid guid)
        {
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