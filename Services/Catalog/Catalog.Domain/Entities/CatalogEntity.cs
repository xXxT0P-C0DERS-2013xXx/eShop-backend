namespace Catalog.Domain.Entities;

public class CatalogEntity : IBaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; }
    public int? OrderNumber { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}