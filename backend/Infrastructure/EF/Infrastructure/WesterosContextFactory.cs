using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Infrastructure;

public class WesterosContextFactory: DesignTimeDbContextFactoryBase<WesterosContext>
{
    protected override WesterosContext CreateNewInstance(DbContextOptions<WesterosContext> options)
    {
        return new WesterosContext(options);
    }
}