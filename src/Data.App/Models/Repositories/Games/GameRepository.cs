using Data.App.Models.Esportshub;
using Data.App.Models.Esportshub.Entities;

namespace Data.App.Models.Repositories.Games
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(EsportshubContext context) : base(context)
        {
        }
    }
}