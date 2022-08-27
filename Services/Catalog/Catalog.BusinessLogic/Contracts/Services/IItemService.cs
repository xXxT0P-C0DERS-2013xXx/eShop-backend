namespace Catalog.BusinessLogic.Contracts.Services;

public interface IItemService
{
    Task<BaseResponse> AddItem(ItemModel model);
    Task<BaseResponse> GetItemById(Guid id);
    Task<BaseResponse> UpdateItemById(ItemModel model, Guid id);
    Task<BaseResponse> DeleteItemById(Guid id);
    Task<BaseResponse> GetAllItems();
}