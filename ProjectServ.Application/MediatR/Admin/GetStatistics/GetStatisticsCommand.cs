using MediatR;
using ProjectServ.Application.Models;

namespace ProjectServ.Application.MediatR.Admin.GetStatistics;

public class GetStatisticsCommand : IRequest<StatisticsResponse>;
