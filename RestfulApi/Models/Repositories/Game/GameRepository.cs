using Models.Entities;

namespace Models.Repositories
{
    public class GameRepository : GenericRepository<Game>, IGameRepository
    {
        public GameRepository(EsportshubContext context) : base(context)
        {
        }
    }
}