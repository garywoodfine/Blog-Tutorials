using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Threenine.Configurations.PostgreSql;
using Threenine;

namespace Database.AddressServices;

public class AddressServiceContext : BaseContext<AddressServiceContext>
{
    public AddressServiceContext(DbContextOptions<AddressServiceContext> options)
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