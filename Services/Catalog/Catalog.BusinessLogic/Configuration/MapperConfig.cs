namespace Catalog.BusinessLogic.Configuration;

public class MapperConfiguration : Profile
{
    public MapperConfiguration()
    {
        CategoryModelMapping();
    }

    private void CategoryModelMapping()
    {
        CreateMap<CategoryModel, CategoryEntity>()
            .ForMember(x => x.Id, opt => opt.Ignore());
        CreateMap<CategoryEntity, CategoryModel>();
    }
}