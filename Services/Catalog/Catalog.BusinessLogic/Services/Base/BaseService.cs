namespace Catalog.BusinessLogic.Services.Base;

public class BaseService
{
    private readonly CatalogContext _context;
    private readonly IDistributedCache _cache;

    protected BaseService(CatalogContext context, IDistributedCache cache)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _cache = cache  ?? throw new ArgumentNullException(nameof(cache));
    }

    protected async Task<int> SaveAsync(IBaseEntity entity)
    {
        entity.CreatedDate = DateTime.UtcNow;
        entity.UpdatedDate = DateTime.UtcNow;

        _context.Add(entity);
        return await _context.SaveChangesAsync();
    }
    
    protected async Task<int> UpdateAsync(IBaseEntity entity)
    {
        entity.UpdatedDate = DateTime.UtcNow;

        _context.Update(entity);
        return await _context.SaveChangesAsync();
    }

    protected async Task<int> DeleteAsync(IBaseEntity? entity)
    {
        if (entity == null)
            return -1;
        
        _context.Remove(entity);
        return await _context.SaveChangesAsync();
    }
}