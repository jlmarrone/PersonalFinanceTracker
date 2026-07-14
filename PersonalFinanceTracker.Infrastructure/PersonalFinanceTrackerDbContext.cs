using System.Diagnostics;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PersonalFinanceTracker.Domain.Aggregates.CategoryCatalogs;
using PersonalFinanceTracker.Domain.Aggregates.Expenses;
using PersonalFinanceTracker.Domain.Aggregates.Reports;
using PersonalFinanceTracker.Domain.Entities.Base;

namespace PersonalFinanceTracker.Infrastructure;

public sealed class PersonalFinanceTrackerDbContext(
    DbContextOptions<PersonalFinanceTrackerDbContext> options) : DbContext(options)
{
    public DbSet<Expense> Expenses => Set<Expense>();

    public DbSet<CategoryCatalog> CategoryCatalogs => Set<CategoryCatalog>();

    public DbSet<Report> Reports => Set<Report>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        ConfigureCommonEntityProperties(modelBuilder);
    }

    private static void ConfigureCommonEntityProperties(ModelBuilder modelBuilder)
    {
        var configureTimeTrackingMethodInfo = typeof(PersonalFinanceTrackerDbContext)
            .GetMethod(
                nameof(ConfigureTimeTrackable),
                BindingFlags.NonPublic | BindingFlags.Static);

        Debug.Assert(configureTimeTrackingMethodInfo != null,
            $"Type {nameof(PersonalFinanceTrackerDbContext)} should have static {nameof(ConfigureTimeTrackable)} method");

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(ITimeTrackable).IsAssignableFrom(entity.ClrType))
            {
                configureTimeTrackingMethodInfo.MakeGenericMethod(entity.ClrType)
                    .Invoke(null, [modelBuilder]);
            }
        }
    }

    private static void ConfigureTimeTrackable<T>(ModelBuilder builder)
        where T : class, IEntity, ITimeTrackable
    {
        builder.Entity<T>()
            .Property(t => t.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("SYSUTCDATETIME()")
            .HasColumnType("datetime2(7)")
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.Entity<T>()
            .HasIndex(t => t.CreatedAt);
    }
}