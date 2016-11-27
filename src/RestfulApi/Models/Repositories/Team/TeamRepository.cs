using RestfulApi.Models.Esportshub;

namespace RestfulApi.Models.Repositories.Team
{
    public class TeamRepository: GenericRepository<Esportshub.Entities.Team.Team>, ITeamRepository
    {
        public TeamRepository(EsportshubContext context) : base(context)
        {
        }
    }
    
}