using RestfulApi.Models.Esportshub;

namespace RestfulApi.Models.Repositories.Event
{
    public class EventRepository : GenericRepository<EsportshubApi.Models.Entities.Event>, IEventRepository
    {
        public EventRepository(EsportshubContext context) : base(context)
        {
        }
    }
}