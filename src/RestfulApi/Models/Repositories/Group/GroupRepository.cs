using esportshubapi.Repositories;
using EsportshubApi.Models.Entities;
using EsportshubApi.Models.Models;

namespace EsportshubApi.Models.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(EsportshubContext context) : base(context)
        {
        }
    }
}