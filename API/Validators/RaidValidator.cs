namespace raiding.API.validators;

public class RaidValidator : AbstractValidator<Raid>
{
    public RaidValidator()
    {
        RuleFor(_ => _.dps).NotEmpty().WithMessage("DPS can't be empty!");
        RuleFor(_ => _.Name).NotEmpty().WithMessage("Title can't be empty!");
        RuleFor(_ => _.healer).NotEmpty().WithMessage("Healer can't be empty!");
        RuleFor(_ => _.tank).NotEmpty().WithMessage("tank can't be empty!");
    }
}