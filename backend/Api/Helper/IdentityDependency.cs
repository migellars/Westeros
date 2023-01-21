using Domain.Models.User;
using Infrastructure.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;

namespace Api.Helper;

public static class IdentityDependency
{
    public static void AddApiIdentityDependency(this IServiceCollection services)
    {
        //Identity
        services.AddIdentityCore<ApplicationUser>(
                setup =>
                {
                    setup.SignIn.RequireConfirmedAccount = false;
                    setup.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    setup.Lockout.AllowedForNewUsers = false;
                    setup.Password.RequireNonAlphanumeric = false;
                    setup.Password.RequireLowercase = false;
                    setup.Password.RequireUppercase = false;
                    setup.Password.RequireDigit = true;
                    setup.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    setup.User.RequireUniqueEmail = false;
                })
            .AddRoles<ApplicationRole>()
            .AddEntityFrameworkStores<WesterosContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager();
        IdentityModelEventSource.ShowPII = true;
    }

}