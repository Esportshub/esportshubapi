using RestfulApi.App.Models.Esportshub;
using RestfulApi.App.Models.Esportshub.Entities;

namespace RestfulApi.App.Models.Repositories.Activities
{
    public class ActivityRepository : GenericRepository<Activity>, IAcitivtyRepository
    {
        public ActivityRepository(EsportshubContext context) : base(context)
        {
        }
    }
}