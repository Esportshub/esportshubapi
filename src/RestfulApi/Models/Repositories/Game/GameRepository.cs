using EsportshubApi.Models.Entities;
using RestfulApi.Models.Esportshub;

namespace RestfulApi.Models.Repositories.Game
{
    public class GameRepository : GenericRepository<Group>, IGameRepository
    {
        public GameRepository(EsportshubContext context) : base(context)
        {
        }
    }
}