using Models.Entities;

namespace Models.Repositories
{
    public class ActivityRepository : GenericRepository<Activity>, IAcitivtyRepository
    {
        public ActivityRepository(EsportshubContext context) : base(context)
        {
        }
    }
}