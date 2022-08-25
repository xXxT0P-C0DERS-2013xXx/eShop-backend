namespace Catalog.BusinessLogic.Services;

public class CategoryService : BaseService, ICategoryService
{
    private readonly CatalogContext _context;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;

    public CategoryService(CatalogContext context, IMapper mapper, IDistributedCache cache) : base(context, cache)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _cache = cache  ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task AddCategory(CategoryModel model)
    {
        var category = _mapper.Map<CategoryModel, CategoryEntity>(model);
        await SaveAsync(category, CacheConstants.Categories);
    }

    public async Task<CategoryModel?> GetCategoryById(Guid id)
    {
        var category = await GetCategoryEntityById(id);
        if (category == null)
            return null;

        return _mapper.Map<CategoryEntity, CategoryModel>(category);
    }

    public async Task<List<CategoryModel>> GetAllCategories()
    {
        var categories = await GetCategoriesEntity();
        return _mapper.Map<List<CategoryEntity>, List<CategoryModel>>(categories);
    }
    
    public async Task UpdateCategoryById(CategoryModel model, Guid id)
    {
        var category = await GetCategoryEntityById(id);
        if (category == null)
            return;

        var newCategory = _mapper.Map<CategoryModel, CategoryEntity>(model);
        
        await UpdateAsync(newCategory, CacheConstants.Categories);
    }

    public async Task DeleteCategoryById(Guid id)
    {
        var category = await GetCategoryEntityById(id);
        if (category == null)
            return;
        
        await DeleteAsync(category, CacheConstants.Categories);
    }

    private async Task<CategoryEntity?> GetCategoryEntityById(Guid id)
    {
        var categories = await GetCategoriesEntity();
        if (categories.Count < 0)
            return null;

        return categories.FirstOrDefault(x => x.Id == id);
    }

    private async Task<List<CategoryEntity>> GetCategoriesEntity()
    {
        var categories = await DistributedCacheExtensions.GetRecordAsync<List<CategoryEntity>>(_cache, CacheConstants.Categories);
        if (categories != null && categories.Count != 0)
            return categories;

        categories = await _context.Categories.ToListAsync();
        await DistributedCacheExtensions.SetRecordAsync(_cache, CacheConstants.Categories, categories);
        return categories;
    }
}