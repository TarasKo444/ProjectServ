using ProjectServ.Application.MediatR.Applications.AcceptApplication;
using ProjectServ.Domain.Enums;
using LadaServ.Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectServ.Application.Common;
using ProjectServ.Application.Models;
using ProjectServ.Application.Services;
using ProjectServ.Infrastructure;

namespace ProjectServ.Application.MediatR.Applications.CloseApplication;

public class CloseApplicationCommandHandler(
        AppDbContext appDbContext, 
        UserAccessorService userAccessorService)
    : IRequestHandler<CloseApplicationCommand, ApplicationResponse>
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly UserAccessorService _userAccessorService = userAccessorService;

    public async Task<ApplicationResponse> Handle(
        CloseApplicationCommand request, 
        CancellationToken cancellationToken)
    {
        var application = await _appDbContext.Applications
            .Include(a => a.User)
            .Include(a => a.Master)
            .FirstOrDefaultAsync(
            a => a.Id == request.Id, cancellationToken: cancellationToken);

        Throw.UserFriendlyExceptionIfNull(application,
            400, "Application not exists");

        Throw.UserFriendlyExceptionIf(application!.CurrentStatus != StatusEnum.InWork.ToString(),
            400, "Application is accepted or closed");
        
        var masterId = _userAccessorService.UserId;
        
        Throw.UserFriendlyExceptionIf(application!.MasterId != masterId,
            400, "Application not yours");

        application!.CurrentStatus = StatusEnum.Ready.ToString();
        application!.ClosedAt = DateTime.UtcNow;

        _appDbContext.Update(application);
        await _appDbContext.SaveChangesAsync(cancellationToken);

        var result = application.Adapt<ApplicationResponse>();
        result.UserName = application.User.UserName!;
        result.MasterName = application.Master?.UserName;
        
        return result;
    }
}
