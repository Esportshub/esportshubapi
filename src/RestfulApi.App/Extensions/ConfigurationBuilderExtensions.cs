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
            return (ConfigurationBuilder) builder
                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()) + "/Data.App")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
        }
    }
}