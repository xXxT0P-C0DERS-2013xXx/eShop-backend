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
    public async Task<IActionResult> AddCategory(CategoryModel model)
    {
        return Ok(await _categoryService.AddCategory(model));
    }
    
    [HttpGet]
    [Route("getCategory/{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        return Ok(await _categoryService.GetCategoryById(id));
    }    
    
    [HttpGet]
    [Route("getCategories")]
    public async Task<IActionResult> GetAllCategories()
    {
        return Ok(await _categoryService.GetAllCategories());
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateCategoryById(CategoryModel model, Guid id)
    {
        return Ok(await _categoryService.UpdateCategoryById(model, id));
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteCategoryById(Guid id)
    {
        return Ok(await _categoryService.DeleteCategoryById(id));
    }
}