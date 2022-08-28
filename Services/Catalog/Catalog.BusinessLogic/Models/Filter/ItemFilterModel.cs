namespace Catalog.BusinessLogic.Models.Filter;

public class ItemFilterModel : BaseFilterModel
{
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
}