using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF;

public interface ISeeder
{
    /// <summary>
    /// Performs the seeding action on the underlying datacontext
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="optionalAction">An action to be performed after implementation of the seeding</param>
    void Seed(ModelBuilder? builder, Action? optionalAction = null);
}