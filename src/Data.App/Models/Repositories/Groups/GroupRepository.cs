using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Groups
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(EsportshubContext context) : base(context)
        {
        }
    }
}