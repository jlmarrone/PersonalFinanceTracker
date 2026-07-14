using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalFinanceTracker.Domain.Aggregates.CategoryCatalogs;
using PersonalFinanceTracker.Domain.Aggregates.CategoryCatalogs.ValueObjects;

namespace PersonalFinanceTracker.Infrastructure.Persistence.Configurations;

public sealed class CategoryCatalogConfiguration : IEntityTypeConfiguration<CategoryCatalog>
{
    public void Configure(EntityTypeBuilder<CategoryCatalog> builder)
    {
        builder.ToTable("CategoryCatalogs");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.HasIndex(c => c.UserId)
            .IsUnique();

        builder.OwnsMany(c => c.Categories, category =>
        {
            category.ToTable("Categories");

            category.WithOwner().HasForeignKey("CategoryCatalogId");

            category.Property<int>("Id");
            category.HasKey("Id");

            category.Property(c => c.Value)
                .HasColumnName("Name")
                .HasMaxLength(100)
                .IsRequired();

            category.HasIndex("CategoryCatalogId", nameof(Category.Value))
                .IsUnique();
        });

        builder.Navigation(c => c.Categories)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasField("_categories");
    }
}
