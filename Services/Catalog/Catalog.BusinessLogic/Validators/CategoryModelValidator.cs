namespace Catalog.BusinessLogic.Validators;

public class CategoryModelValidator : AbstractValidator<CategoryModel>
{
    private const int TitleMaxLength = 100;
    public CategoryModelValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage(x => string.Format(
                ValidatorConstants.ConditionTwoAnd,
                String.Format(ValidatorConstants.ShouldBeNotEmpty, x.Title), 
                String.Format(ValidatorConstants.MaxLength, TitleMaxLength)
            ));
    }
}