namespace Catalog.BusinessLogic.Validators;

public class ItemModelValidator : BaseModelValidator<ItemModel>
{
    private const int MinPrice = 0;
    private const int MinQuantity = 0;
    public ItemModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(x => String.Format(ValidatorConstants.ShouldBeNotEmpty, x.Title));
        
        RuleFor(x => x.Price)
            .GreaterThan(MinPrice)
            .WithMessage(x => String.Format(ValidatorConstants.ShouldBeGreaterThan, x.Title, MinPrice));
        
        RuleFor(x => x.Quantity)
            .GreaterThan(MinQuantity)
            .WithMessage(x => String.Format(ValidatorConstants.ShouldBeGreaterThan, x.Title, MinQuantity));
    }
}