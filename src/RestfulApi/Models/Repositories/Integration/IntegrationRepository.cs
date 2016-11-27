using RestfulApi.Models.Esportshub;

namespace RestfulApi.Models.Repositories.Integration
{
    public class IntegrationRepository : GenericRepository<Esportshub.Entities.Integration.Integration>, IIntegrationRepository
    {
        public IntegrationRepository(EsportshubContext context) : base(context)
        {
        }
    }
}