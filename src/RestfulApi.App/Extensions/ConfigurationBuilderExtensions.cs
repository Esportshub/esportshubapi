using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace RestfulApi.App.Extensions
{
    public static class ConfigurationBuilderExtensions
    {
        public static ConfigurationBuilder SetStartUpConfiguration(this ConfigurationBuilder builder,
            IHostingEnvironment env)
        {
             builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                .AddUserSecrets();
            return builder;
        }
    }
}