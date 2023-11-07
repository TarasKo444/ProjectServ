using ProjectServ.Application.Models;
using ProjectServ.Domain.Enums;
using LadaServ.Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectServ.Application.Common;
using ProjectServ.Application.Services;
using ProjectServ.Infrastructure;

namespace ProjectServ.Application.MediatR.Applications.AcceptApplication;

public class AcceptApplicationCommandHandler(
        AppDbContext appDbContext, 
        UserAccessorService userAccessorService)
    : IRequestHandler<AcceptApplicationCommand, Unit>
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly UserAccessorService _userAccessorService = userAccessorService;

    public async Task<Unit> Handle(
        AcceptApplicationCommand request, 
        CancellationToken cancellationToken)
    {
        var application = await _appDbContext.Applications
            .Include(a => a.User)
            .Include(a => a.Master)
            .FirstOrDefaultAsync(
            a => a.Id == request.Id, cancellationToken: cancellationToken);

        Throw.UserFriendlyExceptionIfNull(application,
            400, "Application not exists");

        Throw.UserFriendlyExceptionIf(application!.CurrentStatus != StatusEnum.Waiting.ToString(),
            400, "Application is accepted or closed");
        
        var masterId = _userAccessorService.UserId;

        application!.CurrentStatus = StatusEnum.InWork.ToString();
        application!.TimeOfAcceptance = DateTime.UtcNow;
        application!.MasterId = masterId;

        _appDbContext.Update(application);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;    
    }
}
