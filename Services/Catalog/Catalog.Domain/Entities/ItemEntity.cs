namespace Catalog.Domain.Entities;

public class ItemEntity : IBaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    
    public Guid CategoryId { get; set; }
    public CategoryEntity Category { get; set; }
}