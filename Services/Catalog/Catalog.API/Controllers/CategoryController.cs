namespace Catalog.API.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
    }

    [HttpPost]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ConflictResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> AddCategory(CategoryModel model)
    {
        return Ok(await _categoryService.AddCategory(model));
    }
    
    [HttpGet]
    [Route("getCategory/{id}")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        return Ok(await _categoryService.GetCategoryById(id));
    }    
    
    [HttpGet]
    [Route("getCategories")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategories()
    {
        return Ok(await _categoryService.GetAllCategories());
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ConflictResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateCategoryById(CategoryModel model, Guid id)
    {
        return Ok(await _categoryService.UpdateCategoryById(model, id));
    }
    
    [HttpDelete]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ConflictResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> DeleteCategoryById(Guid id)
    {
        return Ok(await _categoryService.DeleteCategoryById(id));
    }
}