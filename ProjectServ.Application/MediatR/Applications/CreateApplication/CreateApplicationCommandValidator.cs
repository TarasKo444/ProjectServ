using FluentValidation;

namespace ProjectServ.Application.MediatR.Applications.CreateApplication;

public class CreateApplicationCommandValidator : AbstractValidator<CreateApplicationCommand>
{
    public CreateApplicationCommandValidator()
    {
        RuleFor(a => a.CarBrand)
            .NotEmpty().WithMessage("CarBrand not given")
            .NotNull().WithMessage("CarBrand not given");
        
        RuleFor(a => a.CarNumber)
            .NotEmpty().WithMessage("CarNumber not given")
            .NotNull().WithMessage("CarNumber not given");
        
        RuleFor(a => a.ProblemDescription)
            .NotEmpty().WithMessage("ProblemDescription not given")
            .NotNull().WithMessage("ProblemDescription not given");
        
        RuleFor(a => a.TimeOfArrival)
            .NotEmpty().WithMessage("TimeOfArrival not given")
            .NotNull().WithMessage("TimeOfArrival not given");
    }
}
