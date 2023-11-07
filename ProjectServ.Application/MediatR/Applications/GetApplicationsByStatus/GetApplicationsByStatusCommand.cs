using MediatR;
using ProjectServ.Application.Models;

namespace ProjectServ.Application.MediatR.Applications.GetApplicationsByStatus;

public class GetApplicationsByStatusCommand : IRequest<IEnumerable<ApplicationResponse>>
{
    public string? Status { get; set; }
}
