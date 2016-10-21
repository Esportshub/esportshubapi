using EsportshubApi.Models.Entities;
using EsportshubApi.Models;

namespace EsportshubApi.Models.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(EsportshubContext context) : base(context)
        {
        }
    }
}