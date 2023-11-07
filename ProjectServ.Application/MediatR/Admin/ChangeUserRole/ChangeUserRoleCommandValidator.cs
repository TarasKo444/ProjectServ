using FluentValidation;

namespace ProjectServ.Application.MediatR.Admin.ChangeUserRole;

public class ChangeUserRoleCommandValidator : AbstractValidator<ChangeUserRoleCommand>
{
    public ChangeUserRoleCommandValidator()
    {
        RuleFor(r => r.UserId)
            .NotNull().WithMessage("UserId not given")
            .NotEmpty().WithMessage("UserId not given");
        
        RuleFor(r => r.Role)
            .NotNull().WithMessage("Role not given")
            .NotEmpty().WithMessage("Role not given");
    }
}
