namespace Catalog.Persistence;

public class CatalogContextFactory : IDesignTimeDbContextFactory<CatalogContext>
{
    public CatalogContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CatalogContext>();
        
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=eShop;Username=postgres;Password=postgres");

        return new CatalogContext(optionsBuilder.Options);
    }
}