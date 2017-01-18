using System;
using Data.App.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.App.Extensions
{
    public static class ServiceScopeExtensions
    {

        public static void Migrate(this IServiceScope serviceScope,IApplicationBuilder app)
        {
            if (serviceScope == null) throw new ArgumentNullException(nameof(serviceScope));
            serviceScope.ServiceProvider.GetService<EsportshubContext>().Database.Migrate();
            serviceScope.Dispose();
        }

        public static void SeedData(this IServiceScope serviceScope,IApplicationBuilder app)
        {
            if (serviceScope == null) throw new ArgumentNullException(nameof(serviceScope));
            serviceScope.ServiceProvider.GetService<EsportshubContext>().EnsureSeedData();
            serviceScope.Dispose();
        }
    }
}