using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Renetdux.Extension;
using Renetdux.Infrastructure.Common;
using Renetdux.Infrastructure.DataModel;
using Renetdux.Infrastructure.Services.Logger;
using System;

namespace Renetdux
{
    public class Startup
    {
        public IConfiguration Configuration;
        private readonly IHostingEnvironment Env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(Env.ContentRootPath)
              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
              .AddEnvironmentVariables();

            Configuration = builder.Build();
            var config = new Configuration();

            Configuration.Bind(config);
            services.AddSingleton(x => config);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            IoCContainerFactory.MapInterfaces(services);

            services.AddDbContext<RenetduxDatabaseContext>(options => options.UseInMemoryDatabase(databaseName: "RenetduxDatabase"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services, ILoggerService loggerService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // DataGenerator.Initialize(services);

            app.ConfigureCustomMiddleware(loggerService);

            app.UseMvc();
        }
    }
}
