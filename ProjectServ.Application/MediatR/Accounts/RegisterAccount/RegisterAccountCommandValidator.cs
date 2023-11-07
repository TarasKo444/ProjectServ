using FluentValidation;

namespace ProjectServ.Application.MediatR.Accounts.RegisterAccount;

public class RegisterAccountCommandValidator : AbstractValidator<RegisterAccountCommand>
{
    public RegisterAccountCommandValidator()
    {
        RuleFor(x => x.UserName)
            .NotNull().WithMessage("UserName not given")
            .NotEmpty().WithMessage("UserName not given");
        
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email not given")
            .NotEmpty().WithMessage("Email not given");
        
        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password not given")
            .NotEmpty().WithMessage("Password not given");
    }
}
