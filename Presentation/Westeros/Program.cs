using FluentValidation;
using Infrastructure;
using Infrastructure.EF;
using MediatR;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Serilog;
using SharedKernel.Helpers.API_Helper;
using SharedKernel.Helpers.Logging;

namespace Westeros;

public class Program
{
    private static readonly string? Namespace = typeof(Program).Namespace;
    private static readonly string? AppName = Namespace;
    public static void Main(string[] args)
    {


        var builder = WebApplication.CreateBuilder(args);

        builder.WebHost.UseConfiguration(builder.Configuration);
        builder.Host.UseSerilog(SerilogConfiguration.Configure);
        builder.Services.AddMediatR(Application.AssemblyReference.Assembly);
        builder.Services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly);
        builder.Services.AddInfrastructureDependency(builder.Configuration);
        builder.Services.AddApiBehaviorCollection();
        builder.Services.AddApiIdentityDependency();
        var app = builder.Build();

        Log.Information("Configuring web host ({ApplicationContext})", AppName);
        Log.Information("Started web host ({ApplicationContext})", AppName);
        SeedDatabase();

        void SeedDatabase()
        {
            using var scope = app.Services.CreateScope();
            var scopedContext = scope.ServiceProvider.GetRequiredService<LannisterContext>();
            LannisterInitializer.Initializer(scopedContext);
            //scopedContext.Database.Migrate();
        }

        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwaggerDocumentation(apiVersionDescriptionProvider);
            app.UseSharedMiddleware();
        }

        app.UseAuthentication();
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.MapFallbackToFile("index.html"); ;

        app.Run();

    }
}
