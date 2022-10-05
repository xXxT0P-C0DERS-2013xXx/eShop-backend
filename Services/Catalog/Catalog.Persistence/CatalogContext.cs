namespace Catalog.Persistence;

public sealed class CatalogContext : DbContext
{
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ItemEntity> Items { get; set; }

    public CatalogContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder builder) 
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}