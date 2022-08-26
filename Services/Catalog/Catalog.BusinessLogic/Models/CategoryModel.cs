namespace Catalog.BusinessLogic.Models;

[Validator(typeof(CategoryModelValidator))]
public class CategoryModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int OrderNumber { get; set; }
}