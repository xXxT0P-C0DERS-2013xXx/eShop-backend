namespace Catalog.BusinessLogic.Filtering;

public class ItemFiltering
{
    private IEnumerable<ItemEntity> _items;
    public ItemFiltering(ItemFilterModel model, IEnumerable<ItemEntity> items, int take = 25, int skip = 0)
    {
        _items = items;
        Filter(model);
        _items = _items.Skip(skip).Take(take);
    }

    public List<ItemEntity> GetList() => _items.ToList();

    private void Filter(ItemFilterModel filterModel)
    {
        if (filterModel.CreatedDateFrom.HasValue)
        {
            _items = _items.Where(x => x.CreatedDate >= filterModel.CreatedDateFrom.Value);
        }
        if (filterModel.CreatedDateTo.HasValue)
        {
            _items = _items.Where(x => x.CreatedDate <= filterModel.CreatedDateTo.Value);
        }
        if (filterModel.UpdatedDateFrom.HasValue)
        {
            _items = _items.Where(x => x.CreatedDate >= filterModel.UpdatedDateFrom.Value);
        }
        if (filterModel.UpdatedDateTo.HasValue)
        {
            _items = _items.Where(x => x.CreatedDate <= filterModel.UpdatedDateTo.Value);
        }
        if (filterModel.PriceFrom.HasValue)
        {
            _items = _items.Where(x => x.Price >= filterModel.PriceFrom.Value);
        }
        if (filterModel.PriceTo.HasValue)
        {
            _items = _items.Where(x => x.Price <= filterModel.PriceTo.Value);
        }        
        if (filterModel.QuantityFrom.HasValue)
        {
            _items = _items.Where(x => x.Quantity >= filterModel.QuantityFrom.Value);
        }        
        if (filterModel.QuantityTo.HasValue)
        {
            _items = _items.Where(x => x.Quantity <= filterModel.QuantityTo.Value);
        }
    }
}