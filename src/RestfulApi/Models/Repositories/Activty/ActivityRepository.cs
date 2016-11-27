using EsportshubApi.Models.Entities;
using RestfulApi.Models.Esportshub;

namespace RestfulApi.Models.Repositories.Activty
{
    public class ActivityRepository : GenericRepository<Activity>, IAcitivtyRepository
    {
        public ActivityRepository(EsportshubContext context) : base(context)
        {
        }
    }
}