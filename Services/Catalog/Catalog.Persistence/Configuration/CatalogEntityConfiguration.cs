namespace Catalog.Persistence.Configuration;

public class CatalogEntityConfiguration : IEntityTypeConfiguration<CatalogEntity>
{
    public void Configure(EntityTypeBuilder<CatalogEntity> builder)
    {
        builder.ToTable("Catalogs");

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.OrderNumber)
            .IsRequired(false);
    }
}