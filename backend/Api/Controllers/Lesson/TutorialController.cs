using Api.Payload;
using Application.Dto;
using Application.Features.Lesson.Commands.Create;
using Application.Features.Lesson.Commands.Remove;
using Application.Features.Lesson.Commands.Update;
using Application.Features.Lesson.Queries.GetById;
using Application.Helper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Resources.API_Helper;

namespace Api.Controllers.Lesson;

[AllowAnonymous]
[Route("lesson")]
[ApiVersion("1.0")]
public class TutorialController : BaseController<TutorialController>
{
    private readonly IMediator _mediator;

    public TutorialController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-lesson", Name = "create-lesson-route")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateAuthorProfile([FromBody] CreateLessonCommand lesson, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(lesson, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPut("edit-lesson", Name = "edit-lesson-route")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateAuthorProfile([FromQuery] Guid id, [FromBody] UpdateLessonVm lessonVm, CancellationToken cancellationToken)
    {
        var command = new UpdateLessonCommand(id, lessonVm.Name, lessonVm.Description, lessonVm.AuthorId);
        var result = await _mediator.Send(command, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpDelete("delete-lesson", Name = "delete-lesson-route")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveAuthorProfile([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var data = new RemoveLessonCommand(id);
        var result = await _mediator.Send(data, cancellationToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("all-lesson", Name = "all-lesson-route")]
    [ProducesResponseType(typeof(ApiResponse<TutorialDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllLesson(CancellationToken cancellationToken, [FromQuery]int pageNumber = 0, [FromQuery]int pageSize = 20)
    {
        var query = new Pagination<TutorialDto>(pageNumber, pageSize);

        var result = await _mediator.Send(query, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
    [HttpGet("get-lesson", Name = "get-lesson-route")]
    [ProducesResponseType(typeof(ApiResponse<TutorialDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLesson([FromQuery]Guid id, CancellationToken cancellationToken)
    {
        var query = new GetLessonByIdQuery(id);
        var result = await _mediator.Send(query, cancellationToken);
        return result.Success ? Ok(result) : NotFound(result);
    }
}