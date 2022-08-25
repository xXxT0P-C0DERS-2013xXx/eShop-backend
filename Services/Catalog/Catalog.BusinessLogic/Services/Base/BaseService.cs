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

    protected async Task<int> SaveAsync(IBaseEntity entity, string? key = null)
    {
        entity.CreatedDate = DateTime.UtcNow;
        entity.UpdatedDate = DateTime.UtcNow;

        _context.Add(entity);
        return await DropCacheAndSaveChangesAsync(key);
    }
    
    protected async Task<int> UpdateAsync(IBaseEntity entity, string? key = null)
    {
        entity.UpdatedDate = DateTime.UtcNow;

        _context.Update(entity);
        return await DropCacheAndSaveChangesAsync(key);
    }

    protected async Task<int> DeleteAsync(IBaseEntity? entity, string? key = null)
    {
        if (entity == null)
            return -1;
        
        _context.Remove(entity);        
        return await DropCacheAndSaveChangesAsync(key);
    }

    private async Task<int> DropCacheAndSaveChangesAsync(string? key = null)
    {
        if (!string.IsNullOrEmpty(key))
            await _cache.RemoveAsync(key);
        
        return await _context.SaveChangesAsync();
    }
}