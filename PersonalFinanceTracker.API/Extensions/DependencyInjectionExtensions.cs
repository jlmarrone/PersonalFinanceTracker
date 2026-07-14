using PersonalFinanceTracker.Domain.Aggregates.Expenses.DomainServices;
using PersonalFinanceTracker.Domain.Repositories;
using PersonalFinanceTracker.Infrastructure.Extensions;
using PersonalFinanceTracker.Infrastructure.Repositories;

namespace PersonalFinanceTracker.API.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddInfrastructure(builder.Environment, builder.Configuration);

        // Repositories - Scoped, must match DbContext's lifetime
        builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
        builder.Services.AddScoped<ICategoryCatalogRepository, CategoryCatalogRepository>();
        builder.Services.AddScoped<IReportRepository, ReportRepository>();

        // Read-side query - Scoped, same DbContext-consistency reasoning
        // builder.Services.AddScoped<IReportHistoryReader, ReportHistoryReader>();

        // Domain service - stateless, Scoped by team convention
        builder.Services.AddScoped<ExpenseFactory>();
    }
}