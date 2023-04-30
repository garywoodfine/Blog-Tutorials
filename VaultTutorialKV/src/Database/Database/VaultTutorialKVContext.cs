using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Threenine.Configurations.PostgreSql;
using Threenine;

namespace Database.VaultTutorialKVs;

public class VaultTutorialKVContext : BaseContext<VaultTutorialKVContext>
{
    public VaultTutorialKVContext(DbContextOptions<VaultTutorialKVContext> options)
        : base(options)
    {
    }
  
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(DefaultSchema.Name);
        modelBuilder.HasPostgresExtension(PostgreExtensions.UUIDGenerator);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}