using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace SharedKernel.Resources.Logging;

public class SerilogConfiguration
{
    private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";

    public static Action<HostBuilderContext, LoggerConfiguration> Configure => (context, configuration) =>
        {
            var config = GetConfiguration();

            configuration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .Enrich.WithProperty("Application", context.HostingEnvironment.ApplicationName)
                .ReadFrom.Configuration(context.Configuration);
        };
    private static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable(AspNetCoreEnvironment)}.json", optional: true)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}