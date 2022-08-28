namespace Catalog.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
    }

    [HttpPost]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ConflictResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddItem(ItemModel model)
    {
        return Ok(await _itemService.AddItem(model));
    }
    
    [HttpGet]
    [Route("getItem/{id}")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetItemById(Guid id)
    {
        return Ok(await _itemService.GetItemById(id));
    }    
    
    [HttpGet]
    [Route("getItems")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllItems()
    {
        return Ok(await _itemService.GetAllItems());
    }
    
    [HttpPost]
    [Route("getItemsWithPagination")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetItemsWithPagination(ItemFilterModel model, int take, int skip)
    {
        return Ok(await _itemService.GetItemsWithPagination(model, take, skip));
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ConflictResponse), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateItemById(ItemModel model, Guid id)
    {
        return Ok(await _itemService.UpdateItemById(model, id));
    }
    
    [HttpDelete]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ConflictResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteItemById(Guid id)
    {
        return Ok(await _itemService.DeleteItemById(id));
    }
}