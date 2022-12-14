
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.AddressServices;

internal class AddressServiceContextFactory : IDesignTimeDbContextFactory<AddressServiceContext>
{
    public AddressServiceContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<AddressServiceContext> dbContextOptionsBuilder =
            new();

        dbContextOptionsBuilder.UseNpgsql(ConnectionStringNames.LocalBuild);
        return new AddressServiceContext(dbContextOptionsBuilder.Options);
    }
}
