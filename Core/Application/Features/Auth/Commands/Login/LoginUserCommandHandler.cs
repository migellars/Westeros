using Application.Dto;
using Domain.Models.User;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using SharedKernel.Helpers.API_Helper;
using SharedKernel.Helpers.CQRS;
using SharedKernel.Helpers.JwtAuthenticationManager;
using SharedKernel.Helpers.JwtAuthenticationManager.Models;

namespace Application.Features.Auth.Commands.Login;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand, TokenDto>
{
    private readonly UserManager<ApplicationUser>? _userManager;
    private readonly JwtTokenHandler _jwtHandler;


    public LoginUserCommandHandler(UserManager<ApplicationUser>? userManager, JwtTokenHandler jwtHandler)
    {
        _userManager = userManager;
        _jwtHandler = jwtHandler;
    }

    public async Task<ApiResponse<TokenDto>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        //validate user data
        var validateData = new LoginUserCommandValidator();
        await validateData.ValidateAndThrowAsync(request, cancellationToken: cancellationToken);

        // get user from db
        var getUser = await _userManager.FindByNameAsync(request.UserName);
        if (getUser == null)
            return ApiResponse.GetFailed(new TokenDto());

        // verify password
        var verifyPassword = await _userManager.CheckPasswordAsync(getUser, request.Password);
        switch (verifyPassword)
        {
            case true:
                {
                    //get claims
                    var claims = new TokenClaims();
                    claims.Username = getUser.UserName;
                    claims.Email = getUser.Email;
                    claims.UserId = getUser.Id;
                    //get token
                    var userToken = await _jwtHandler.GenerateJwtToken(claims);
                    var result = new TokenDto();
                    result.Token = userToken.Token;
                    result.UserName = userToken.UserName;
                    result.ExpiresIn = TimeSpan.FromSeconds(double.Parse(userToken.ExpiresIn.ToString()));
                    return ApiResponse.GetSuccess(result);
                }
            default:
                return ApiResponse.GetFailed( new TokenDto());
        }
    }
}

