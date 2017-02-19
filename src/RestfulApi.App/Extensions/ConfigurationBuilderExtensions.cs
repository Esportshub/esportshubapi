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
<<<<<<< HEAD
            return (ConfigurationBuilder) builder
//                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()) + Path)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
=======
            builder.SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true);
            builder.AddUserSecrets();
            return builder;
>>>>>>> aedaa64ed5306ca6ea41e7b66aa2af9dc16b97b9
        }
    }
}