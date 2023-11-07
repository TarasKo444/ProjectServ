using ProjectServ.Domain.Enums;
using LadaServ.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectServ.Application.Models;
using ProjectServ.Infrastructure;

namespace ProjectServ.Application.MediatR.Admin.GetStatistics;

public class GetStatisticsCommandHandler(AppDbContext appDbContext) 
    : IRequestHandler<GetStatisticsCommand, StatisticsResponse>
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<StatisticsResponse> Handle(GetStatisticsCommand request, CancellationToken cancellationToken)
    {
        return new()
        {
            UsersCount = await _appDbContext.Users
                .CountAsync(u => u.Role == RoleEnum.User, cancellationToken),
            MastersCount = await _appDbContext.Users
                .CountAsync(u => u.Role == RoleEnum.Master, cancellationToken),
            ApplicationsCount = await _appDbContext.Applications.CountAsync(cancellationToken),
            WaitingApplicationsCount = await _appDbContext.Applications
                .CountAsync(a => a.CurrentStatus == nameof(StatusEnum.Waiting), cancellationToken),
            InWorkApplicationsCount = await _appDbContext.Applications
                .CountAsync(a => a.CurrentStatus == nameof(StatusEnum.InWork), cancellationToken),
            ClosedApplicationsCount = await _appDbContext.Applications
                .CountAsync(a => a.CurrentStatus == nameof(StatusEnum.Ready), cancellationToken),
        };
    }
}
