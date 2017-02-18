using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Groups
{
    public class GroupRepository : IGroupRepository
    {
        private readonly IRepository<Group> _internalRepository;
        private readonly EsportshubContext _esportshubContext;
        public GroupRepository(IRepository<Group> internalRepository)
        {
            _internalRepository = internalRepository;
            _esportshubContext = internalRepository.Context;
        }

        public async Task<IEnumerable<Group>> FindByAsync(Expression<Func<Group, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public async Task<Group> FindAsync(Guid guid)
        {
            return await _internalRepository.FindAsync(guid);
        }

        public async Task<bool> SaveAsync()
        {
            return await _internalRepository.SaveAsync();
        }


        public IEnumerable<Group> FindBy(Expression<Func<Group, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public Group Find(Guid guid)
        {
            return _internalRepository.Find(guid);
        }

        public void Insert(Group entity)
        {
            _internalRepository.Insert(entity);
        }

        public void Delete(Guid guid)
        {
            _internalRepository.Delete(guid);
        }

        public void Update(Group entity)
        {
            _internalRepository.Update(entity);
        }

        public bool Save()
        {
            return _internalRepository.Save();
        }
    }
}