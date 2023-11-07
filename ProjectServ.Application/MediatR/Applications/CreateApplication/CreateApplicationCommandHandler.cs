using ProjectServ.Domain.Enums;
using LadaServ.Infrastructure;
using Mapster;
using MediatR;
using ProjectServ.Application.Services;
using ProjectServ.Infrastructure;

namespace ProjectServ.Application.MediatR.Applications.CreateApplication;

public class CreateApplicationCommandHandler(AppDbContext appDbContext, UserAccessorService userAccessorService) : IRequestHandler<CreateApplicationCommand, Unit>
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly UserAccessorService _userAccessorService = userAccessorService;

    public async Task<Unit> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        request.CarBrand = request.CarBrand!.Trim();
        request.CarNumber = request.CarNumber!.Trim();
        request.ProblemDescription = request.ProblemDescription!.Trim();
        
        var userId = _userAccessorService.UserId;
        
        var application = request.Adapt<ProjectServ.Domain.Entities.Application>();
        application.UserId = userId;
        application.CreatedAt = DateTime.UtcNow;
        application.CurrentStatus = StatusEnum.Waiting.ToString();

        await _appDbContext.Applications.AddAsync(application, cancellationToken);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
