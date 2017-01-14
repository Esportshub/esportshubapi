using Data.App.Models.Esportshub;
using Data.App.Models.Esportshub.Entities;

namespace Data.App.Models.Repositories.Integrations
{
    public class IntegrationRepository : GenericRepository<Integration>, IIntegrationRepository
    {
        public IntegrationRepository(EsportshubContext context) : base(context)
        {
        }
    }
}