namespace raiding.API.validators;

public class DPSValidator : AbstractValidator<DPS>
{
    public DPSValidator()
    {
        RuleFor(_ => _.CharacterName).NotEmpty().WithMessage("Name can't be empty!");
        RuleFor(_ => _.ILevel).NotEmpty().WithMessage("ILevel can't be empty!");
        RuleFor(_ => _.ILevel).GreaterThanOrEqualTo(200).WithMessage("ILevel needs to be above 200");
        RuleFor(_ => _.DamagePerSecond).NotEmpty().WithMessage("Damage can't be empty!");
        RuleFor(_ => _.DamagePerSecond).GreaterThanOrEqualTo(9000).WithMessage("Damage needs to be above 9000");
    }
}