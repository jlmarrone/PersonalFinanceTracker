using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PersonalFinanceTracker.Application.Abstractions.Authentication;
using PersonalFinanceTracker.Infrastructure.Authentication;

namespace PersonalFinanceTracker.Infrastructure.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IHostEnvironment hostEnvironment,
        IConfiguration configuration)
    {
        services.AddScoped<IUserIdentifierProvider, UserIdentifierProvider>();

        services.AddDbContext<PersonalFinanceTrackerDbContext>((_, opt) =>
        {
            var connectionString = configuration.GetConnectionString(ConnectionStrings.PersonalFinanceTracker)
                                   ?? throw new InvalidOperationException($"Connection string {ConnectionStrings.PersonalFinanceTracker} not found.");

            opt.UseSqlServer(connectionString);

            if (!hostEnvironment.IsDevelopment())
            {
                return;
            }

            opt.EnableSensitiveDataLogging();
            opt.LogTo(Console.WriteLine, [RelationalEventId.CommandExecuting.Name!]!);
        });
    }
}