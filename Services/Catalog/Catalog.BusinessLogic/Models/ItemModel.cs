namespace Catalog.BusinessLogic.Models;

public class ItemModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public Guid CategoryId { get; set; }
}