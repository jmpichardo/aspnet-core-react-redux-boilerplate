using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Renetdux.Extension;
using Renetdux.Infrastructure.Common;
using Renetdux.Infrastructure.DataModel;
using Renetdux.Infrastructure.Services.Logger;
using System;
using System.Text;

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

            var secret = config.JwtSecret;
            var key = Encoding.ASCII.GetBytes(secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            IoCContainerFactory.MapInterfaces(services);

            services.AddDbContext<RenetduxDatabaseContext>(options => options.UseInMemoryDatabase(databaseName: "RenetduxDatabase"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider services, ILoggerService loggerService)
        {
            app.UseServiceDiscovery();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // DataGenerator.Initialize(services);

            app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

            app.UseAuthentication();

            app.ConfigureCustomMiddleware(loggerService);

            app.UseMvc();
        }
    }
}
