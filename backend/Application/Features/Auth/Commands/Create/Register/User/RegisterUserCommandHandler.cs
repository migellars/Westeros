using Application.Features.Auth.Commands.Create.Register.User.Events.Email;
using Application.Helper.Constants.Auth;
using Domain.Models.User;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Resources.API_Helper;
using SharedKernel.Resources.CQRS;
using SharedKernel.Resources.Util;

namespace Application.Features.Auth.Commands.Create.Register.User;

public class RegisterUserCommandHandler: ICommandHandler<RegisterUserCommand>
{
    private readonly UserManager<ApplicationUser> _requestManager;
    private readonly IPublisher _publisher;
    private readonly IHttpContextAccessor _contextAccessor;

    public RegisterUserCommandHandler(UserManager<ApplicationUser> requestManager, IPublisher publisher, IHttpContextAccessor contextAccessor)
    {
        _requestManager = requestManager;
        _publisher = publisher;
        _contextAccessor = contextAccessor;
    }

    public async Task<ApiResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        //validate
        var validateUser = new RegisterUserCommandValidator();
        await validateUser.ValidateAndThrowAsync(request, cancellationToken);
        var requestData = new ApplicationUser();
        requestData.UserName = request.UserName;
        requestData.Email = request.Email;
        var res = await _requestManager.CreateAsync(requestData, request.Password);
        switch (res.Succeeded)
        {
            case true:
            {
                var confirmEmailLink = await _requestManager.GenerateEmailConfirmationTokenAsync(requestData);
                var link = Utils.AbsoluteUrlCustom(_contextAccessor, _contextAccessor.HttpContext.Request.Host.Value, "api/confirm-email", new { token = confirmEmailLink });
                await _publisher.Publish(new UserRegisteredEmailEvent()
                {
                    CreatedAt = DateTime.Now,
                    Body = link
                }, cancellationToken);
                break;
            }
            default:
                return ApiResponse.GetFailed(null, null, AuthConstants.UserExist);
        }

        return ApiResponse.GetSuccess(null, null, AuthConstants.UserCreated);
    }
}