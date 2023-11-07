using FluentValidation;

namespace ProjectServ.Application.MediatR.Accounts.LoginAccount;

public class LoginAccountCommandValidator : AbstractValidator<LoginAccountCommand>
{
    public LoginAccountCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull().WithMessage("Email not given")
            .NotEmpty().WithMessage("Email not given");
        
        RuleFor(x => x.Password)
            .NotNull().WithMessage("Password not given")
            .NotEmpty().WithMessage("Password not given");
    }
}
