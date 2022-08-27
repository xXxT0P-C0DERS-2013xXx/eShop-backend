namespace Catalog.BusinessLogic.Configuration;

public class MapperConfiguration : Profile
{
    public MapperConfiguration()
    {
        CategoryModelMapping();
        ItemModelMapping();
    }

    private void CategoryModelMapping()
    {
        CreateMap<CategoryModel, CategoryEntity>()
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<CategoryEntity, CategoryModel>();
    }
    
    private void ItemModelMapping()
    {
        CreateMap<ItemModel, ItemEntity>()
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<ItemEntity, ItemModel>();
    }
}