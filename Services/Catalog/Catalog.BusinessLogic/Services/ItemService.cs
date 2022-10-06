namespace Catalog.BusinessLogic.Services;

public class ItemService : BaseService, IItemService
{
    private readonly CatalogContext _context;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    private readonly IResponseFactory _responseFactory;

    public ItemService(CatalogContext context, IMapper mapper, IDistributedCache cache, IResponseFactory responseFactory) : base(context, cache)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _responseFactory = responseFactory ?? throw new ArgumentNullException(nameof(responseFactory));
    }

    public async Task<BaseResponse> AddItem(ItemModel model)
    {
        var item = _mapper.Map<ItemModel, ItemEntity>(model);
        var isItemExist = (await GetItemsEntity()).Any(x => x.Title.Equals(model.Title) && x.CategoryId == model.CategoryId);
        if (isItemExist)
            return _responseFactory.ConflictResponse(String.Format(ItemValidationConstants.ItemExist, nameof(ItemEntity)), model);
        
        var result = await SaveAsync(item, CacheConstants.Items);
        if (result <= 0)
            return _responseFactory.ConflictResponse(String.Format(ErrorsConstants.SaveError, nameof(ItemEntity), nameof(AddItem)), model);
        
        return _responseFactory.SuccessResponse(_mapper.Map<ItemEntity, ItemModel>(item));
    }

    public async Task<BaseResponse> GetItemById(Guid id)
    {
        var item = await GetItemEntityById(id);
        if (item == null)
            return _responseFactory.NotFoundResponse(String.Format(ErrorsConstants.NotFoundWithId, nameof(ItemEntity), id));

        return _responseFactory.SuccessResponse(_mapper.Map<ItemEntity, ItemModel>(item));
    }

    public async Task<BaseResponse> UpdateItemById(ItemModel model, Guid id)
    {
        var item = await GetItemEntityById(id);
        if (item == null)
            return _responseFactory.NotFoundResponse(String.Format(ErrorsConstants.NotFoundWithId, nameof(ItemEntity), id));
        
        var isItemExist = (await GetItemsEntity()).Any(x => x.Title.Equals(model.Title) && x.CategoryId == model.CategoryId);
        if (isItemExist)
            return _responseFactory.ConflictResponse(String.Format(ItemValidationConstants.ItemExist, nameof(ItemEntity)), model);

        item.Title = model.Title;
        item.Description = model.Description;
        item.Quantity = model.Quantity;
        item.Price = model.Price;
        
        var result = await UpdateAsync(item, CacheConstants.Items);
        if (result <= 0)
            return _responseFactory.ConflictResponse(String.Format(ErrorsConstants.SaveError, nameof(ItemEntity), nameof(UpdateItemById)), model);

        return _responseFactory.SuccessResponse(result);
    }

    public async Task<BaseResponse> DeleteItemById(Guid id)
    {
        var item = await GetItemEntityById(id);
        if (item == null)
            return _responseFactory.NotFoundResponse(String.Format(ErrorsConstants.NotFoundWithId, nameof(ItemEntity), id));
        
        var result = await DeleteAsync(item, CacheConstants.Items);
        if (result <= 0)
            return _responseFactory.ConflictResponse(String.Format(ErrorsConstants.SaveError, nameof(ItemEntity), nameof(DeleteItemById)), id);
        
        return _responseFactory.SuccessResponse(result);
    }

    public async Task<BaseResponse> GetAllItems()
    {
        var items = await GetItemsEntity();
        return _responseFactory.SuccessResponse(_mapper.Map<List<ItemEntity>, List<ItemModel>>(items));
    }

    public async Task<BaseResponse> GetItemsWithPagination(ItemFilterModel filterModel, int take = 25, int skip = 0)
    {
        var categories = await GetItemsEntity();
        var filteredCategories = new ItemFiltering(filterModel, categories, take, skip).GetList();

        var categoriesModel = _mapper.Map<List<ItemEntity>, List<ItemModel>>(filteredCategories);

        return _responseFactory.SuccessResponse(categoriesModel);
    }

    #region Helpers

    private async Task<ItemEntity?> GetItemEntityById(Guid id)
    {
        var items = await GetItemsEntity();
        if (items.Count < 0)
            return null;

        return items.FirstOrDefault(x => x.Id == id);
    }

    private async Task<List<ItemEntity>> GetItemsEntity()
    {
        var items = await DistributedCacheExtensions.GetRecordAsync<List<ItemEntity>>(_cache, CacheConstants.Items);
        if (items != null && items.Count != 0)
            return items;

        items = await _context.Items.ToListAsync();
        await DistributedCacheExtensions.SetRecordAsync(_cache, CacheConstants.Categories, items);
        return items;
    }

    #endregion
}