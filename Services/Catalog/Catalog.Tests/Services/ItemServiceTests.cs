namespace Catalog.Tests.Services;

public class ItemServiceTests
{
    private readonly CatalogContext _context;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    private readonly ItemService _service;
    private readonly IResponseFactory _responseFactory;
    
    public ItemServiceTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<CatalogContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        serviceCollection.AddAutoMapper(Assembly.Load("Catalog.BusinessLogic"));
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _context = serviceProvider.GetRequiredService<CatalogContext>();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
        _responseFactory = new ResponseFactory();
        _cache = new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions()));
        _service = new ItemService(_context, _mapper, _cache, _responseFactory);
    }
    
    [Fact]
    public async Task AddItem_True()
    {
        // Arrange
        var itemsCount = _context.Items.Count();

        // Act
        var itemModel = new ItemModel()
        {
            Title = "faketitle",
            Price = 110.0M,
            Description = String.Empty,
            Quantity = 10
        };
        await _service.AddItem(itemModel);
        var itemsCountAfterOperation = _context.Items.Count();

        // Assert
        Assert.NotEqual(itemsCount, itemsCountAfterOperation);
    }
    
    [Fact]
    public async Task GetAllItems_True()
    {
        // Arrange
        var itemsList = new List<ItemEntity>()
        {
            new()
            {
                Title = "faketitle",
                Price = 110.0M,
                Description = String.Empty,
                Quantity = 1
            },
            new()
            {
                Title = "faketitle2",
                Price = 60.0M,
                Description = "String.Empty",
                Quantity = 170
            }
        };
        await _context.AddRangeAsync(itemsList);
        await _context.SaveChangesAsync();
        
        // Act
        var response = await _service.GetAllItems();

        // Assert
        Assert.Equal(itemsList.Count, (((response.Data) as List<ItemModel>)!).Count);
    }
    
    [Fact]
    public async Task GetItemById_True()
    {
        // Arrange
        var itemEntity = new ItemEntity()
        {
            Title = "fake",
            Price = 8.0M,
            Description = String.Empty,
            Quantity = 7
        };
        await _context.AddAsync(itemEntity);
        await _context.SaveChangesAsync();

        // Act
        var item = await _context.Items.Where(x => x.Title == "fake").FirstOrDefaultAsync();
        var result = await _service.GetItemById(item!.Id);

        // Assert
        Assert.Equal((result.Data as ItemModel)!.Title, itemEntity.Title);
    }

    [Fact]
    public async Task DeleteItemById_True()
    {
        // Arrange
        var itemEntity = new ItemEntity()
        {
            Title = "fake",
            Price = 8.0M,
            Description = String.Empty,
            Quantity = 7
        };
        await _context.AddAsync(itemEntity);
        await _context.SaveChangesAsync();

        // Act
        var item = await _context.Items.Where(x => x.Title == "fake").FirstOrDefaultAsync();
        if (item != null) 
            await _service.DeleteItemById(item.Id);

        // Assert
        Assert.Equal(0, _context.Items.Count());
    }
    
    [Fact]
    public async Task UpdateById_True()
    {
        // Arrange
        var itemEntity = new ItemEntity()
        {
            Title = "fake",
            Price = 8.0M,
            Description = String.Empty,
            Quantity = 7
        };
        await _context.AddAsync(itemEntity);
        await _context.SaveChangesAsync();

        // Act
        var item = await _context.Items.Where(x => x.Title == "fake").FirstOrDefaultAsync();

        var model = new ItemModel()
        {
            Title = "newFakeTitle",
            Price = 1000000.0M,
            Description = String.Empty,
            Quantity = 1
        };
        
        await _service.UpdateItemById(model, item.Id);
        item = await _context.Items.Where(x => x.Title == "newFakeTitle" && x.Id == item.Id).FirstOrDefaultAsync();

        // Assert
        Assert.Equal(item?.Title, model.Title);
        Assert.Equal(item?.Price, model.Price);
        Assert.Equal(item?.Description, model.Description);
        Assert.Equal(item?.Quantity, model.Quantity);
        Assert.Single(_context.Items.ToList());
    }
}