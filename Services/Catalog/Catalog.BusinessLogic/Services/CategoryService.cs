namespace Catalog.BusinessLogic.Services;

public class CategoryService : BaseService, ICategoryService
{
    private readonly CatalogContext _context;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    private readonly IResponseFactory _responseFactory;

    public CategoryService(CatalogContext context, IMapper mapper, IDistributedCache cache, IResponseFactory responseFactory) : base(context, cache)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _responseFactory = responseFactory ?? throw new ArgumentNullException(nameof(responseFactory));
    }

    public async Task<BaseResponse> AddCategory(CategoryModel model)
    {
        var category = _mapper.Map<CategoryModel, CategoryEntity>(model);
        var isCategoryExist = (await GetCategoriesEntity()).Any(x => x.Title.Equals(model.Title));
        if (isCategoryExist)
            return _responseFactory.ConflictResponse(String.Format(CategoryValidationConstants.CategoryExist, nameof(CategoryEntity)), model);
        
        var result = await SaveAsync(category, CacheConstants.Categories);
        if (result <= 0)
            return _responseFactory.ConflictResponse(String.Format(ErrorsConstants.SaveError, nameof(CategoryEntity), nameof(AddCategory)), model);
        
        return _responseFactory.SuccessResponse(_mapper.Map<CategoryEntity, CategoryModel>(category));
    }

    public async Task<BaseResponse> GetCategoryById(Guid id)
    {
        var category = await GetCategoryEntityById(id);
        if (category == null)
            return _responseFactory.NotFoundResponse(String.Format(ErrorsConstants.NotFoundWithId, nameof(CategoryEntity), id));

        return _responseFactory.SuccessResponse(_mapper.Map<CategoryEntity, CategoryModel>(category));
    }

    public async Task<BaseResponse> GetAllCategories()
    {
        var categories = (await GetCategoriesEntity()).OrderBy(x => x.OrderNumber).ToList();
        return _responseFactory.SuccessResponse(_mapper.Map<List<CategoryEntity>, List<CategoryModel>>(categories));
    }
    
    public async Task<BaseResponse> UpdateCategoryById(CategoryModel model, Guid id)
    {
        var category = await GetCategoryEntityById(id);
        if (category == null)
            return _responseFactory.NotFoundResponse(String.Format(ErrorsConstants.NotFoundWithId, nameof(CategoryEntity), id));
        
        var isItemExist = (await GetCategoriesEntity()).Any(x => x.Title.Equals(model.Title));
        if (isItemExist)
            return _responseFactory.ConflictResponse(String.Format(ItemValidationConstants.ItemExist, nameof(ItemEntity)), model);

        category.Title = model.Title;
        category.OrderNumber = model.OrderNumber;
        
        var result = await UpdateAsync(category, CacheConstants.Categories);
        if (result <= 0)
            return _responseFactory.ConflictResponse(String.Format(ErrorsConstants.SaveError, nameof(CategoryEntity), nameof(UpdateCategoryById)), model);

        return _responseFactory.SuccessResponse(result);
    }

    public async Task<BaseResponse> DeleteCategoryById(Guid id)
    {
        var category = await GetCategoryEntityById(id);
        if (category == null)
            return _responseFactory.NotFoundResponse(String.Format(ErrorsConstants.NotFoundWithId, nameof(CategoryEntity), id));
        
        var result = await DeleteAsync(category, CacheConstants.Categories);
        if (result <= 0)
            return _responseFactory.ConflictResponse(String.Format(ErrorsConstants.SaveError, nameof(CategoryEntity), nameof(DeleteCategoryById)), id);
        
        return _responseFactory.SuccessResponse(result);
    }

    #region Helpers

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

    #endregion
}