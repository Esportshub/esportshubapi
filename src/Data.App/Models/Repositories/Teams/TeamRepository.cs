using Data.App.Models.Esportshub;
using Data.App.Models.Esportshub.Entities;

namespace Data.App.Models.Repositories.Teams
{
    public class TeamRepository: GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(EsportshubContext context) : base(context)
        {
        }
    }
    
}