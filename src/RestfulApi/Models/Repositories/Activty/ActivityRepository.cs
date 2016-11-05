using EsportshubApi.Models.Entities;
using EsportshubApi.Models;

namespace EsportshubApi.Models.Repositories
{
    public class ActivityRepository : GenericRepository<Activity>, IAcitivtyRepository
    {
        public ActivityRepository(EsportshubContext context) : base(context)
        {
        }
    }
}