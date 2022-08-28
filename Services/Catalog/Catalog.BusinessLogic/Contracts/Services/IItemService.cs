namespace Catalog.BusinessLogic.Contracts.Services;

public interface IItemService
{
    Task<BaseResponse> AddItem(ItemModel model);
    Task<BaseResponse> GetItemById(Guid id);
    Task<BaseResponse> UpdateItemById(ItemModel model, Guid id);
    Task<BaseResponse> DeleteItemById(Guid id);
    Task<BaseResponse> GetAllItems();
    Task<BaseResponse> GetItemsWithPagination(ItemFilterModel filterModel, int take = 25, int skip = 0);
}