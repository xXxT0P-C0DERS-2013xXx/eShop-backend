namespace Catalog.BusinessLogic.Models.Filter.Base;

public class BaseFilterModel
{
    public DateTime? CreatedDateFrom { get; set; }
    public DateTime? CreatedDateTo { get; set; }
    public DateTime? UpdatedDateFrom { get; set; }
    public DateTime? UpdatedDateTo { get; set; }
}