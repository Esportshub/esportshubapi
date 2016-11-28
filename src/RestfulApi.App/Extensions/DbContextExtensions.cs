using System;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RestfulApi.App.Models.Esportshub;

namespace RestfulApi.App.Extensions
{
    public static class DbCtxExtensions
    {
        private static IServiceScope _serviceScope;

        public static void CreateAdminAccount(IApplicationBuilder app)
        {
            EsportshubContext.CreateAdminAccount(app.ApplicationServices, Startup.Configuration).Wait();
        }

        public static void Migrate(IApplicationBuilder app)
        {
            _serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _serviceScope.ServiceProvider.GetService<EsportshubContext>().Database.Migrate();
            Dispose();
        }

        public static void SeedData(IApplicationBuilder app)
        {
            _serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            _serviceScope.ServiceProvider.GetService<EsportshubContext>().EnsureSeedData();
            Dispose();
        }

        private static void Dispose()
        {
            _serviceScope.Dispose();
        }
    }
}