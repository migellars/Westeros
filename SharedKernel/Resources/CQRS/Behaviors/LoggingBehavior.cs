using System.Diagnostics;
using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Logging;

namespace SharedKernel.Resources.CQRS.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"[START] Handling {typeof(TRequest).Name}");
        var stopwatch = Stopwatch.StartNew();
        TResponse response;
        try
        {
            try
            {
                var requestData = JsonSerializer.Serialize(request);
                _logger.LogInformation($"[DATA] With data: {requestData}");
            }
            catch (System.Exception)
            {
                _logger.LogInformation("[Serialization ERROR] Could not serialize the request.");
            }
            response = await next();
        }
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation(
                $"[END] Handled {typeof(TResponse).Name}; Execution time = {stopwatch.ElapsedMilliseconds}ms");
        }
        return response;
    }
}