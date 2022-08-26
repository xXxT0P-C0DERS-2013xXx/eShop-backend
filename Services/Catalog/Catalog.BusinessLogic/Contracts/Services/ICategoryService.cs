namespace Catalog.BusinessLogic.Contracts.Services;

public interface ICategoryService
{
    Task<BaseResponse> AddCategory(CategoryModel model);
    Task<BaseResponse> GetCategoryById(Guid id);
    Task<BaseResponse> UpdateCategoryById(CategoryModel model, Guid id);
    Task<BaseResponse> DeleteCategoryById(Guid id);
    Task<BaseResponse> GetAllCategories();
}