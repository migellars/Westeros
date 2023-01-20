using Microsoft.EntityFrameworkCore;

namespace Infrastructure.EF.Infrastructure;

public class LannisterContextFactory: DesignTimeDbContextFactoryBase<LannisterContext>
{
    protected override LannisterContext CreateNewInstance(DbContextOptions<LannisterContext> options)
    {
        return new LannisterContext(options);
    }
}