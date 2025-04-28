using UNITEC.NV.DAL.NVContext;
using UNITEC.NV.EL.Models.AppSettings;
using Microsoft.EntityFrameworkCore;

namespace UNITEC.NV.API.Configuration
{
    public static class DbContextConfiguration
    {
        public static void ConfigureDbContext(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            var controlAccesoConnection = configuration.GetValue<string>("ConnectionStrings:NVDB")?.Trim() ?? "";

            services.AddDbContext<NVContext>(options =>
            {
                options.UseSqlServer(controlAccesoConnection);
            });

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
        }
    }
}
