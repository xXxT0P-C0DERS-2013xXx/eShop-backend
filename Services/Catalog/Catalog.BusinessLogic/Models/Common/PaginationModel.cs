namespace Catalog.BusinessLogic.Models.Common;

public class ItemFilterModel : BaseFilterModel
{
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
    public int? QuantityFrom { get; set; }
    public int? QuantityTo { get; set; }
}