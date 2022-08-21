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
        await SaveAsync(category);
    }

    public async Task<CategoryEntity?> GetCategoryById(Guid id)
    {
        return await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public async Task UpdateCategoryById(CategoryModel model, Guid id)
    {
        var category = _context.Categories.FirstOrDefault(x => x.Id == id);
        if (category == null)
            return;
        
        await UpdateAsync(category);
    }

    public async Task DeleteCategoryById(Guid id)
    {
        await DeleteAsync(_context.Categories.FirstOrDefault(x => x.Id == id));
    }

    public async Task<List<CategoryEntity>> GetAllCategories()
    {
        return await _context.Categories.ToListAsync();
    }
}