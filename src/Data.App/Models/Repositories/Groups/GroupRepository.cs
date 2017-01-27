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
        public GroupRepository(EsportshubContext context, IRepository<Group> internalRepository)
        {
            _esportshubContext = context;
            _internalRepository = internalRepository;
            _internalRepository.SetEsportshubContext(context);
        }

        public async Task<IEnumerable<Group>> FindByAsync(Expression<Func<Group, bool>> filter, string includeProperties)
        {
            return await _internalRepository.FindByAsync(filter, includeProperties);
        }

        public async Task<Group> FindAsync(int id)
        {
            return await _internalRepository.FindAsync(id);
        }

        public async Task<bool> SaveAsync()
        {
            return await _internalRepository.SaveAsync();
        }


        public IEnumerable<Group> FindBy(Expression<Func<Group, bool>> filter, string includeProperties)
        {
            return _internalRepository.FindBy(filter, includeProperties);
        }

        public Group Find(int id)
        {
            return _internalRepository.Find(id);
        }

        public void Insert(Group entity)
        {
            _internalRepository.Insert(entity);
        }

        public void Delete(int id)
        {
            _internalRepository.Delete(id);
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