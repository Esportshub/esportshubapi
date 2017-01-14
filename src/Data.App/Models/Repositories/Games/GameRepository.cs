
using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Games
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(EsportshubContext context) : base(context)
        {
        }
    }
}