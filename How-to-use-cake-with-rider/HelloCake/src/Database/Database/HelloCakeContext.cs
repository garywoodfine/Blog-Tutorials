using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Threenine.Configurations.PostgreSql;
using Threenine;

namespace Database.HelloCakes;

public class HelloCakeContext : BaseContext<HelloCakeContext>
{
    public HelloCakeContext(DbContextOptions<HelloCakeContext> options)
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