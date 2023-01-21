using Application.Dto;
using Application.Features.Author.Commands.Create;
using Application.Features.Author.Commands.Remove;
using Application.Features.Author.Commands.Update;
using Application.Features.Author.Queries.GetById;
using Application.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Resources.API_Helper;

namespace Api.Controllers.Author;

[AllowAnonymous]
[Route("author")]
[ApiVersion("1.0")]
public class AuthorController : BaseController<AuthorController>
{
    private readonly IMediator _mediator;

    public AuthorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-author-profile", Name = "create-author-profile")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAuthorProfile([FromBody] CreateAuthorCommand author, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(author, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }
    [HttpPut("edit-author-profile", Name = "edit-author-profile")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAuthorProfile([FromQuery] Guid id, [FromBody] UpdateAuthorCommand author, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(author, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("delete-author-profile", Name = "delete-author-profile")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveAuthorProfile([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var data = new RemoveAuthorCommand(id);
        var result = await _mediator.Send(data, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("all-author-profile", Name = "all-author-profile")]
    [ProducesResponseType(typeof(ApiResponse<AuthorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAuthor(CancellationToken cancellationToken, int pageNumber = 0, int pageSize = 20)
    {
        var query = new Pagination<AuthorDto>(pageNumber, pageSize);

        var result = await _mediator.Send(query, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
    [HttpGet("author-profile", Name = "get-author-profile")]
    [ProducesResponseType(typeof(ApiResponse<AuthorDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAuthor(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetAuthorByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
}