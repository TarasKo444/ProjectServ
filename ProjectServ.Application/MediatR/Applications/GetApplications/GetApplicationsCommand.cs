using MediatR;
using ProjectServ.Application.Models;

namespace ProjectServ.Application.MediatR.Applications.GetApplications;

public class GetApplicationsCommand : IRequest<IEnumerable<ApplicationResponse>>
{
}
