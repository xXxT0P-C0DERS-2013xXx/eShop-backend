namespace Catalog.Persistence.Configuration;

public class CatalogEntityConfiguration : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("Category");

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.OrderNumber)
            .IsRequired(false);

        builder.HasIndex(x => x.Id);
        builder.HasIndex(x => x.Title);
    }
}