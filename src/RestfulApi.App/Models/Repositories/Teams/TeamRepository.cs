using RestfulApi.App.Models.Esportshub;
using RestfulApi.App.Models.Esportshub.Entities;

namespace RestfulApi.App.Models.Repositories.Teams
{
    public class TeamRepository: GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(EsportshubContext context) : base(context)
        {
        }
    }
    
}