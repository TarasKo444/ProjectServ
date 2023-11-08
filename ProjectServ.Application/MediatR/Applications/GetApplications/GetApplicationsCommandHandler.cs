using ProjectServ.Domain.Enums;
using LadaServ.Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectServ.Application.Common;
using ProjectServ.Application.Models;
using ProjectServ.Application.Services;
using ProjectServ.Infrastructure;

namespace ProjectServ.Application.MediatR.Applications.GetApplications;

public class GetApplicationsCommandHandler(
    AppDbContext appDbContext, 
    UserAccessorService userAccessorService) : IRequestHandler<GetApplicationsCommand, IEnumerable<ApplicationResponse>>
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly UserAccessorService _userAccessorService = userAccessorService;

    public async Task<IEnumerable<ApplicationResponse>> Handle(GetApplicationsCommand request, CancellationToken cancellationToken)
    {
        var userId = _userAccessorService.UserId;
        var role = _userAccessorService.Role;

        var query = _appDbContext.Applications
            .Include(a => a.User)
            .Include(a => a.Master)
            .AsNoTracking();

        switch (role)
        {
            case RoleEnum.User:
                query = query.Where(a => a.UserId == userId);
                break;

            case RoleEnum.Master:
                query = query.Where(a => a.MasterId == userId);
                break;

            default:
                throw new UserFriendlyException(403, "Access denied");
        }

        return query.AsEnumerable().Select(a =>
        {
            var result = a.Adapt<ApplicationResponse>();
            result.UserName = a.User.UserName!;
            result.MasterName = a.Master?.UserName;
            return result;
        }).ToList();
    }
}
