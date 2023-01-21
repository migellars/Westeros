using Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EF;

public static class DatabaseDependencyInjection
{
    public static void AddDatabaseDependency(this IServiceCollection services, IConfiguration? configuration)
    {
        services.AddDbContext<WesterosContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(WesterosContext).Assembly.FullName)));

        services.AddScoped<IWesterosContext>(provider => provider.GetService<WesterosContext>());
    }
}