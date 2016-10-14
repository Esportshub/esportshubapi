using Microsoft.EntityFrameworkCore;

namespace Models {
    public class EsportshubContext : DbContext
    {
        public EsportshubContext(DbContextOptions<EsportshubContext> options) : base(options) {}
    }
}