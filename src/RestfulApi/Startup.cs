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
using MySQL.Data.EntityFrameworkCore.Extensions;
using System.IO;
using EsportshubApi.Models;
using Moq;
using EsportshubApi.Models.Repositories;
using EsportshubApi.Models.Entities;

namespace esportshubapi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore();
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();
            string connection = config["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<EsportshubContext>(options => options.UseMySQL(connection));
            IPlayerBuilder playerBuild = new PlayerBuilder();
            Mock<IPlayerRepository> playerRepository = new Mock<IPlayerRepository>();
            playerRepository.Setup(x => x.GetByIdAsync(1)).Returns(Task.FromResult(playerBuild.SetPlayerId(1).SetNickname("Hejsa").Build())); 
            services.AddTransient<IPlayerRepository, PlayerRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
