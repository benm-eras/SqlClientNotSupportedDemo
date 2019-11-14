using System;
using System.Collections.Generic;
using System.Text;
using DAL;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Functions.Startup))]
namespace Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            this.AddDbContext(builder, config);
        }

        private void AddDbContext(IFunctionsHostBuilder builder, IConfiguration config)
        {
            builder.Services.AddDbContext<DataContext>(o => o.UseSqlServer(
                config.GetConnectionString("SqlConnection"),
                s => s.MigrationsAssembly("Portal.UI")
            ));
        }
    }
}
