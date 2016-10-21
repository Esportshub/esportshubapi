
using EsportshubApi.Models.Entities;

namespace EsportshubApi.Models.Repositories
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(EsportshubContext context) : base(context)
        {
            
        }
    }
}