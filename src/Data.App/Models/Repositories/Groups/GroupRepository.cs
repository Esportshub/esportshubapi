using Data.App.Models.Esportshub;
using Data.App.Models.Esportshub.Entities;

namespace Data.App.Models.Repositories.Groups
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(EsportshubContext context) : base(context)
        {
        }
    }
}