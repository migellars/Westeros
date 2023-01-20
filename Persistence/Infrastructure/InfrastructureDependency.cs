using Application.Contracts.IRepository;
using Infrastructure.Contracts.Repository;
using Infrastructure.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureDependency
{
    public static void AddInfrastructureDependency(this IServiceCollection services,
        IConfiguration? configuration)
    {
        services.AddDatabaseDependency(configuration);
        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();
    }
}