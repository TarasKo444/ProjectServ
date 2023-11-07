using ProjectServ.Domain.Enums;
using LadaServ.Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectServ.Application.Common;
using ProjectServ.Application.Models;
using ProjectServ.Infrastructure;

namespace ProjectServ.Application.MediatR.Applications.GetApplicationsByStatus;

public class GetApplicationsByStatusCommandHandler(AppDbContext appDbContext)
    : IRequestHandler<GetApplicationsByStatusCommand, IEnumerable<ApplicationResponse>>
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<IEnumerable<ApplicationResponse>> Handle(
        GetApplicationsByStatusCommand request, 
        CancellationToken cancellationToken)
    {
        if (Enum.TryParse(request.Status, true, out StatusEnum status))
        {
            return _appDbContext.Applications
                .Include(a => a.User)
                .Include(a => a.Master)
                .AsNoTracking()
                .AsEnumerable()
                .Where(a => a.CurrentStatus == status.ToString())
                .Select(a =>
                {
                    var result = a.Adapt<ApplicationResponse>();
                    result.UserName = a.User.UserName!;
                    result.MasterName = a.Master?.UserName;
                    return result;
                });
        }
        
        throw new UserFriendlyException(400, "Wrong status");
    }
}
