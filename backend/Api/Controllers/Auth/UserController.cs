using Application.Dto;
using Application.Features.Auth.Commands.Create.Register.User;
using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Remove.User;
using Application.Features.Auth.Queries.User.GetById;
using Application.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Resources.API_Helper;

namespace Api.Controllers.Auth;

[Route("auth")]
[ApiVersion("1.0")]
public class UserController : BaseController<UserController>
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("register-user", Name = "register-endpoint")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.Success == false ? BadRequest(result) : Created(string.Empty, result);
    }

    [HttpDelete("delete-user", Name = "delete-user-endpoint")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var query = new RemoveUserCommand(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.Success == false ? BadRequest(result) : Ok(result);
    }

    [HttpGet("user", Name = "user-endpoint")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.Success == false ? BadRequest(result) : Ok(result);
    }
    [HttpGet("all-user-profile", Name = "get-all-user-profile")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllIndividualProfile(CancellationToken cancellationToken, int pageNumber = 0, int pageSize = 20)
    {
        var query = new Pagination<UserDto>(pageNumber, pageSize);

        var result = await _mediator.Send(query, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }

    [AllowAnonymous]
    [HttpPost("login-user", Name = "login-user-profile")]
    [ProducesResponseType(typeof(ApiResponse<TokenDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllIndividualProfile([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
}