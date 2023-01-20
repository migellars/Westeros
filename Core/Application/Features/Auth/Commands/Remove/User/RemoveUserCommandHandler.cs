using Application.Features.Auth.Commands.Remove.User.Event.Email;
using Application.Helper.Constants.Auth;
using Domain.Models.User;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Helpers.API_Helper;
using SharedKernel.Helpers.CQRS;

namespace Application.Features.Auth.Commands.Remove.User;

public class RemoveUserCommandHandler : ICommandHandler<RemoveUserCommand>
{
    private readonly UserManager<ApplicationUser> _requestManager;
    private readonly IPublisher _publisher;

    public RemoveUserCommandHandler(UserManager<ApplicationUser> requestManager, IPublisher publisher)
    {
        _requestManager = requestManager;
        _publisher = publisher;
    }

    public async Task<ApiResponse> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        //validate
        var validateUser = new RemoveUserCommandValidator();
        await validateUser.ValidateAndThrowAsync(request, cancellationToken);
        var res = await _requestManager.FindByIdAsync(request.Id.ToString());
        switch (res)
        {
            case null:
                return ApiResponse.GetFailed(null, null, AuthConstants.UserNotFound);
            default:
                await _requestManager.DeleteAsync(res);
                await _publisher.Publish(new UserRemovedEmailEvent()
                {
                    CreatedAt = DateTime.Now,
                    Body = "link"
                }, cancellationToken);
                break;
        }

        return ApiResponse.GetSuccess(null, null, AuthConstants.UserDeleted);
    }
}