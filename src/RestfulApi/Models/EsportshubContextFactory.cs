using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySQL.Data.Entity.Extensions;

namespace EsportshubApi.Models
{
    public class EsportshubContextFactory : IDbContextFactory<EsportshubContext>
    {
        public EsportshubContext Create(DbContextFactoryOptions options)
        {
            string connection =
                "server=localhost;userid=root;pwd=denlilleiceman20;port=3306;database=esportshub_api;sslmode=none;";
            var builder = new DbContextOptionsBuilder<EsportshubContext>();
            builder.UseMySQL(connection);
            return new EsportshubContext(builder.Options);
        }
    }
}