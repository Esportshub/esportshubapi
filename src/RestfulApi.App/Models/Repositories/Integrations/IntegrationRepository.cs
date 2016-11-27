using RestfulApi.App.Models.Esportshub;
using RestfulApi.App.Models.Esportshub.Entities;

namespace RestfulApi.App.Models.Repositories.Integrations
{
    public class IntegrationRepository : GenericRepository<Integration>, IIntegrationRepository
    {
        public IntegrationRepository(EsportshubContext context) : base(context)
        {
        }
    }
}