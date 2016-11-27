using RestfulApi.App.Models.Esportshub;
using RestfulApi.App.Models.Esportshub.Entities;

namespace RestfulApi.App.Models.Repositories.Players
{
    public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(EsportshubContext context) : base(context)
        {
            
        }
    }
}