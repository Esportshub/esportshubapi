using EsportshubApi.Models.Entities;
using EsportshubApi.Models.Models;

namespace EsportshubApi.Models.Repositories
{
    public class EventRepository : GenericRepository<Event>, IEventRepository
    {
        public EventRepository(EsportshubContext context) : base(context)
        {
        }
    }
}