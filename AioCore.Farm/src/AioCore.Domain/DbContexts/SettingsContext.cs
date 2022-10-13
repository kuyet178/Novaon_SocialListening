using AioCore.Domain.Aggregates.ConfigAggregate;
using AioCore.Domain.Aggregates.DeviceAggregate;
using AioCore.Domain.Aggregates.MachineAggregate;
using AioCore.Domain.Aggregates.PlatformAccountAggregate;
using Microsoft.EntityFrameworkCore;

namespace AioCore.Domain.DbContexts;

public class SettingsContext : DbContext
{
    public const string Schema = "Settings";

    public SettingsContext(DbContextOptions<SettingsContext> options) : base(options)
    {
    }

    public DbSet<Config> Configs { get; set; } = default!;

    public DbSet<Device> Devices { get; set; } = default!;

    public DbSet<Machine> Machines { get; set; } = default!;

    public DbSet<PlatformAccount> PlatformAccounts { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Schema);
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
            relationship.DeleteBehavior = DeleteBehavior.Restrict;

        modelBuilder.Entity<Device>().HasIndex(x => x.Id).IsUnique();
        modelBuilder.Entity<Device>().HasIndex(x => x.Timestamp).IsUnique();
        modelBuilder.Entity<Device>().Property(x => x.Id).IsRequired();
    }
}