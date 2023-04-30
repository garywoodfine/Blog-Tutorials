
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Database.VaultTutorialKVs;

internal class VaultTutorialKVContextFactory : IDesignTimeDbContextFactory<VaultTutorialKVContext>
{
    public VaultTutorialKVContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<VaultTutorialKVContext> dbContextOptionsBuilder =
            new();

        dbContextOptionsBuilder.UseNpgsql(ConnectionStringNames.LocalBuild);
        return new VaultTutorialKVContext(dbContextOptionsBuilder.Options);
    }
}
