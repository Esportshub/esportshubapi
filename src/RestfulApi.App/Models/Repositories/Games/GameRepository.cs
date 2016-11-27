using RestfulApi.App.Models.Esportshub;
using RestfulApi.App.Models.Esportshub.Entities;

namespace RestfulApi.App.Models.Repositories.Games
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(EsportshubContext context) : base(context)
        {
        }
    }
}