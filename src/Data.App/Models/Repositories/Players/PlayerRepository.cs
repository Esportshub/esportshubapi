using Data.App.Models.Esportshub;
using Data.App.Models.Esportshub.Entities;

namespace Data.App.Models.Repositories.Players
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(EsportshubContext context) : base(context)
        {
            
        }
    }
}