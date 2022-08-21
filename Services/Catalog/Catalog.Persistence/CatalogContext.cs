namespace Catalog.Persistence;

public class CatalogContext : DbContext
{
    public DbSet<CatalogEntity> Catalogs { get; set; }

    public CatalogContext(DbContextOptions options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder) 
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}