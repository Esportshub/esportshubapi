using EsportshubApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using RestfulApi;


namespace EsportshubApi.Models
{
    /*public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create(DbContextFactoryOptions options)
        {
            string connection = Startup.Configuration["ConnectionStrings:DefaultConnection"];
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(connection);
            return new ApplicationDbContext(builder.Options);
        }
    }*/
}