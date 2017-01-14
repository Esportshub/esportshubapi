using Data.App.Models.Entities.Events;

namespace Data.App.Models.Repositories.Events
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(EsportshubContext context) : base(context)
        {
        }
    }
}