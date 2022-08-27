namespace Catalog.BusinessLogic.Configuration;

public static class Injector
{
    public static void InjectServices(IServiceCollection service)
    {
        service.AddScoped<ICategoryService, CategoryService>();
        service.AddScoped<IItemService, ItemService>();
        service.AddSingleton<IResponseFactory, ResponseFactory>();
    }
}