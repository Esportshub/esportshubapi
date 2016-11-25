using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RestfulApi;

namespace EsportshubApi.Models
{
    /*public class EsportshubContextFactory : IDbContextFactory<EsportshubContext>
    {
        public EsportshubContext Create(DbContextFactoryOptions options)
        {
            string connection = Startup.Configuration["ConnectionStrings:DefaultConnection"];
            var builder = new DbContextOptionsBuilder<EsportshubContext>();
            builder.UseSqlServer(connection);
            return new EsportshubContext(builder.Options);
        }
    }*/
}