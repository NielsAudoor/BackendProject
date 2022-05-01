namespace raiding.API.validators;

public class HealerValidator : AbstractValidator<Healer>
{
    public HealerValidator()
    {
        RuleFor(_ => _.CharacterName).NotEmpty().WithMessage("Name can't be empty!");
        RuleFor(_ => _.ILevel).NotEmpty().WithMessage("ILevel can't be empty!");
        RuleFor(_ => _.ILevel).GreaterThanOrEqualTo(200).WithMessage("ILevel needs to be above 200");
        RuleFor(_ => _.HealingPerSecond).NotEmpty().WithMessage("Healing can't be empty!");
        RuleFor(_ => _.HealingPerSecond).GreaterThanOrEqualTo(8000).WithMessage("Healing needs to be above 8000");
    }
}