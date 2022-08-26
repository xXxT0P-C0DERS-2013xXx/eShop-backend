namespace Catalog.Domain.Entities;

public class CategoryEntity : IBaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int? OrderNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }

    public List<ItemEntity> Items { get; set; }
}