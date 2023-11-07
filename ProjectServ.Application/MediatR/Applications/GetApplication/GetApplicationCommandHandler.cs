using ProjectServ.Domain.Enums;
using LadaServ.Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectServ.Application.Common;
using ProjectServ.Application.Models;
using ProjectServ.Application.Services;
using ProjectServ.Infrastructure;

namespace ProjectServ.Application.MediatR.Applications.GetApplication;

public class GetApplicationCommandHandler(UserAccessorService userAccessorService, AppDbContext appDbContext) : IRequestHandler<GetApplicationCommand, ApplicationResponse>
{
    private readonly UserAccessorService _userAccessorService = userAccessorService;
    private readonly AppDbContext _appDbContext = appDbContext;
    
    public async Task<ApplicationResponse> Handle(GetApplicationCommand request, CancellationToken cancellationToken)
    {
        var userId = _userAccessorService.UserId;
        var role = _userAccessorService.Role;
        var application = await _appDbContext.Applications
            .AsNoTracking()
            .FirstOrDefaultAsync(a => request.Id == a.Id, cancellationToken: cancellationToken);
        
        Throw.UserFriendlyExceptionIfNull(application,
            400, "Applcation not exists");

        if (role == RoleEnum.User)
        {
            Throw.UserFriendlyExceptionIf(application!.UserId != userId,
                403, "Access denied");
        }

        return application.Adapt<ApplicationResponse>();
    }
}
