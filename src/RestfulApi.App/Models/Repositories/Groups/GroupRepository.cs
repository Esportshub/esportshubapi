using RestfulApi.App.Models.Esportshub;
using RestfulApi.App.Models.Esportshub.Entities;

namespace RestfulApi.App.Models.Repositories.Groups
{
    public class GroupRepository : GenericRepository<Group>, IGroupRepository
    {
        public GroupRepository(EsportshubContext context) : base(context)
        {
        }
    }
}