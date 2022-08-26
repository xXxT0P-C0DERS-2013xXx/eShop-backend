namespace Catalog.BusinessLogic.Validators;

public class CategoryModelValidator : AbstractValidator<CategoryModel>
{
    public CategoryModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage(x => string.Format(ValidatorConstants.ShouldBeNotEmpty, x.Title));
    }
}