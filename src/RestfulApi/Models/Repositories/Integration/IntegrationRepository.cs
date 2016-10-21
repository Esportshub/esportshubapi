using EsportshubApi.Models.Entities;

namespace EsportshubApi.Models.Repositories
{
    public class IntegrationRepository : GenericRepository<Integration>, IIntegrationRepository
    {
        public IntegrationRepository(EsportshubContext context) : base(context)
        {
        }
    }
}