using RestfulApi.Models.Esportshub;

namespace RestfulApi.Models.Repositories.Player
{
    public class PlayerRepository : GenericRepository<Esportshub.Entities.Player.Player>, IPlayerRepository
    {
        public PlayerRepository(EsportshubContext context) : base(context)
        {
            
        }
    }
}