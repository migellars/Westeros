using Application.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.EF;

public static class DatabaseDependencyInjection
{
    public static void AddDatabaseDependency(this IServiceCollection services, IConfiguration? configuration)
    {
        services.AddDbContext<LannisterContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(LannisterContext).Assembly.FullName)));

        services.AddScoped<ILannisterContext>(provider => provider.GetService<LannisterContext>());
    }
}