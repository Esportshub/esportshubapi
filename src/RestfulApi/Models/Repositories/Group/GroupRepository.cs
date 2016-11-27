using RestfulApi.Models.Esportshub;

namespace RestfulApi.Models.Repositories.Group
{
    public class GroupRepository : GenericRepository<Esportshub.Entities.Group.Group>, IGroupRepository
    {
        public GroupRepository(EsportshubContext context) : base(context)
        {
        }
    }
}