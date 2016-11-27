using RestfulApi.App.Models.Esportshub;
using RestfulApi.App.Models.Esportshub.Entities.Events;

namespace RestfulApi.App.Models.Repositories.Events
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(EsportshubContext context) : base(context)
        {
        }
    }
}