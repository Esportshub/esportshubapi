using EsportshubApi.Models.Entities;
using EsportshubApi.Models.Repositories;

namespace EsportshubApi.Models.Repositories
{

    public class TeamRepository: GenericRepository<Team>, ITeamRepository
    {
        public TeamRepository(EsportshubContext context) : base(context)
        {
        }
    }
    
}