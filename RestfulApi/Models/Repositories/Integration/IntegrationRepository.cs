using Models.Entities;

namespace Models.Repositories
{
    public class IntegrationRepository : GenericRepository<Integration>, IIntegrationRepository
    {
        public IntegrationRepository(EsportshubContext context) : base(context)
        {
        }
    }
}