using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SharedKernel.Resources.API_Helper;


[ApiController]
[Authorize]
public class BaseController<T> : ControllerBase where T : BaseController<T>
{
    private ILogger<T>? _logger;
    protected ILogger<T>? Logger => _logger ??= HttpContext.RequestServices.GetService<ILogger<T>>();
}