
using Models.Entities;

namespace Models.Repositories
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(EsportshubContext context) : base(context)
        {
        }
    }
}