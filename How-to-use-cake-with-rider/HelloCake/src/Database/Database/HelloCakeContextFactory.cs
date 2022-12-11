
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.HelloCakes;

internal class HelloCakeContextFactory : IDesignTimeDbContextFactory<HelloCakeContext>
{
    public HelloCakeContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<HelloCakeContext> dbContextOptionsBuilder =
            new();

        dbContextOptionsBuilder.UseNpgsql(ConnectionStringNames.LocalBuild);
        return new HelloCakeContext(dbContextOptionsBuilder.Options);
    }
}
