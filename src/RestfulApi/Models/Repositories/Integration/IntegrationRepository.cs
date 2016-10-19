using EsportshubApi.Models.Entities;
using EsportshubApi.Models.Models;

namespace EsportshubApi.Models.Repositories
{
    public class IntegrationRepository : GenericRepository<Integration>, IIntegrationRepository
    {
        public IntegrationRepository(EsportshubContext context) : base(context)
        {
        }
    }
}