using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SapientGuardian.MySql.Data;
using System.IO;
using EsportshubApi.Models;
using Moq;
using EsportshubApi.Models.Repositories;
using EsportshubApi.Models.Entities;
using MySQL.Data.Entity.Extensions;

namespace esportshubapi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore();
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            string connection = config["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<EsportshubContext>(options => options.UseMySQL(connection));
            services.AddTransient<IPlayerRepository, PlayerRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseMvc(routes =>
                 routes.MapRoute("player", "{controller=Player}/{action=Get}/{id?}")
            );
        }
    }
}
