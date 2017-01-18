using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Teams
{
    public class TeamRepository: GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(EsportshubContext context) : base(context)
        {
        }
    }
    
}