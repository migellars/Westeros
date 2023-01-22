using System.Net.Mime;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SharedKernel.Resources.Cache;
using SharedKernel.Resources.CQRS.Behaviors;
using SharedKernel.Resources.Exception;
using SharedKernel.Resources.JwtAuthenticationManager;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SharedKernel.Resources.API_Helper;

public static class ApiBehaviorMiddleware
{
    private const string AspNetCoreEnvironment = "ASPNETCORE_ENVIRONMENT";
    static readonly string MyPolicy = "_myPolicy";
    public static IConfiguration Configuration { get; set; }

    private static IConfiguration GetConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable(AspNetCoreEnvironment)}.json", optional: true)
            .AddEnvironmentVariables();

        return builder.Build();
    }

    public static void AddApiBehaviorCollection(this IServiceCollection services)
    {
        Configuration = GetConfiguration();
        //Service DI
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        services.AddProblemDetails();
        services.AddSingleton(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddSingleton<JwtTokenHandler>();
        services.AddMemoryCache();
        services.AddScoped<ICacheManager, CacheManager>();
        services.AddCustomJwtAuthentication();

        services.AddControllers(options =>
       {
           options.Filters.Add(new ProducesAttribute(MediaTypeNames.Application.Json));
           options.Filters.Add(new ConsumesAttribute(MediaTypeNames.Application.Json));

       }).AddControllersAsServices();
        services.AddTransient<ExceptionHandlingMiddleware>();

        services
            .AddMvc(options =>
            {
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                options.UseGeneralRoutePrefix("api/");
                options.ReturnHttpNotAcceptable = true;
                options.EnableEndpointRouting = true;
            })
            .AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                setupAction.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Error;
                setupAction.SerializerSettings.Converters.Add(new StringEnumConverter());
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            }).AddXmlDataContractSerializerFormatters().AddFormatterMappings(x =>
            {
                x.GetMediaTypeMappingForFormat("application/json");
            })
            .ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var result = new BadRequestObjectResult(context.ModelState);
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    result.ContentTypes.Add(MediaTypeNames.Application.Xml);
                    return result;
                };
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = false;
                options.SuppressMapClientErrors = false;
                options.ClientErrorMapping[StatusCodes.Status404NotFound].Link = "https://httpstatuses.com/404";
            });
        #region Cors
        //Cors
        services.AddCors(options =>
        {
            options.AddPolicy(name: MyPolicy,
                builder =>
                {
                    //builder.WithOrigins("https://localhost:44318", "https://localhost:5001", "http://localhost:4200")
                    //    .AllowAnyHeader()
                    //    .WithMethods("PUT", "DELETE", "POST", "GET");
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyOrigin()
                    .SetIsOriginAllowed(_ => true); // allow any origin
                });
        });
        #endregion

        #region Api Version
        //API VERSION
        services.AddApiVersioning(o =>
        {
            o.ReportApiVersions = true;
            o.AssumeDefaultVersionWhenUnspecified = true;
            o.DefaultApiVersion = new ApiVersion(1, 0);
            o.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("api-version"),
                new MediaTypeApiVersionReader("ver"));
        });
        services.AddVersionedApiExplorer(
            options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        #endregion

        #region Swagger
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerDocumentation();
        #endregion

    }
    public static void UseSharedMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseProblemDetails();
        app.UseCors(MyPolicy);

    }

}