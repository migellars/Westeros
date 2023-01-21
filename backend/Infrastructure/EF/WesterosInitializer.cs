using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Resources.Logging;

namespace Infrastructure.EF;

public static class WesterosInitializer
{
    public static void Initializer(WesterosContext context)
    {
        context.Database.Migrate();
        SeedEverything(context);
    }

    private static void SeedEverything(WesterosContext context)
    {
        context.Database.EnsureCreated();

        switch (context.Roles.Any())
        {
            case false:
                SeedRoles(context);
                break;
            default:
                WesterosLogger.LogInfo("Role exist");
                break;
        }
    }

    private static void SeedRoles(WesterosContext context)
    {
        var roles = new[]
        {
           new ApplicationRole(){ConcurrencyStamp = Guid.NewGuid().ToString(),Id = new Guid(), Name = "Admin", NormalizedName = "ADMIN"},
           new ApplicationRole(){ConcurrencyStamp = Guid.NewGuid().ToString(),Id = new Guid(), Name = "Author", NormalizedName = "AUTHOR"},
           new ApplicationRole(){ConcurrencyStamp = Guid.NewGuid().ToString(),Id = new Guid(), Name = "Leraner", NormalizedName = "LEARNER"}

        };

        context.Roles.AddRange(roles);

        context.SaveChanges();
    }
}