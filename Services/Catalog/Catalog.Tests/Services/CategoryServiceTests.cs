namespace Catalog.Tests.Services;

public class CategoryServiceTests
{
    private readonly CatalogContext _context;
    private readonly IMapper _mapper;
    private readonly IDistributedCache _cache;
    private readonly ICategoryService _service;
    private readonly IResponseFactory _responseFactory;
    
    public CategoryServiceTests()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddDbContext<CatalogContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        serviceCollection.AddAutoMapper(Assembly.Load("Catalog.BusinessLogic"));
        
        var serviceProvider = serviceCollection.BuildServiceProvider();
        _context = serviceProvider.GetRequiredService<CatalogContext>();
        _mapper = serviceProvider.GetRequiredService<IMapper>();
        _responseFactory = new ResponseFactory();
        _cache = new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions()));
        _service = new CategoryService(_context, _mapper, _cache, _responseFactory);
    }

    [Fact]
    public async Task AddCategory_True()
    {
        // Arrange
        var postsCount = _context.Categories.Count();

        // Act
        var categoryModel = new CategoryModel
        {
            Title = "faketitle",
            OrderNumber = 1
        };
        await _service.AddCategory(categoryModel);
        var postsCountAfterOperation = _context.Categories.Count();

        // Assert
        Assert.NotEqual(postsCount, postsCountAfterOperation);
    }
    
    [Fact]
    public async Task GetAllCategories_True()
    {
        // Arrange
        var categoryModels = new List<CategoryEntity>()
        {
            new()
            {
                Title = "faketitle",
                OrderNumber = 1
            },
            new()
            {
                Title = "faketitle2",
                OrderNumber = 1
            }
        };
        await _context.AddRangeAsync(categoryModels);
        await _context.SaveChangesAsync();
        
        // Act
        var response = await _service.GetAllCategories();

        // Assert
        Assert.Equal(categoryModels.Count, (((response.Data) as List<CategoryModel>)!).Count);
    }
    
    [Fact]
    public async Task GetCategoryById_True()
    {
        // Arrange
        var categoryEntity = new CategoryEntity
        {
            Title = "fake",
            OrderNumber = 1
        };
        await _context.AddAsync(categoryEntity);
        await _context.SaveChangesAsync();

        // Act
        var category = await _context.Categories.Where(x => x.Title == "fake").FirstOrDefaultAsync();
        var result = await _service.GetCategoryById(category!.Id);

        // Assert
        Assert.Equal((result.Data as CategoryModel)!.Title, categoryEntity.Title);
    }

    [Fact]
    public async Task DeleteById_True()
    {
        // Arrange
        var categoryEntity = new CategoryEntity
        {
            Title = "fake",
            OrderNumber = 1
        };
        await _context.AddAsync(categoryEntity);
        await _context.SaveChangesAsync();

        // Act
        var category = await _context.Categories.Where(x => x.Title == "fake").FirstOrDefaultAsync();
        if (category != null) 
            await _service.DeleteCategoryById(category.Id);

        // Assert
        Assert.Equal(0, _context.Categories.Count());
    }
    
    [Fact]
    public async Task UpdateById_True()
    {
        // Arrange
        var categoryEntity = new CategoryEntity
        {
            Title = "fake",
            OrderNumber = 1
        };
        await _context.AddAsync(categoryEntity);
        await _context.SaveChangesAsync();

        // Act
        var category = await _context.Categories.Where(x => x.Title == "fake").FirstOrDefaultAsync();

        var model = new CategoryModel
        {
            Title = "newFakeTitle"
        };
        
        await _service.UpdateCategoryById(model, category.Id);
        category = await _context.Categories.Where(x => x.Title == "newFakeTitle" && x.Id == category.Id).FirstOrDefaultAsync();

        // Assert
        Assert.Equal(category?.Title, model.Title);
        Assert.Single(_context.Categories.ToList());
    }
}