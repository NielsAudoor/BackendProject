namespace raiding.API.validators;

public class TankValidator : AbstractValidator<Tank>
{
    public TankValidator()
    {
        RuleFor(_ => _.CharacterName).NotEmpty().WithMessage("Name can't be empty!");
        RuleFor(_ => _.ILevel).NotEmpty().WithMessage("ILevel can't be empty!");
        RuleFor(_ => _.ILevel).GreaterThanOrEqualTo(200).WithMessage("ILevel needs to be above 200");
        RuleFor(_ => _.armour).NotEmpty().WithMessage("Armour can't be empty!");
        RuleFor(_ => _.armour).GreaterThanOrEqualTo(1000).WithMessage("Armour needs to be above 1000");
    }
}