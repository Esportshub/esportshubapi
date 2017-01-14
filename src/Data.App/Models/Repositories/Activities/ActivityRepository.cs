using Data.App.Models.Entities;

namespace Data.App.Models.Repositories.Activities
{
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(EsportshubContext context) : base(context)
        {
        }
    }
}