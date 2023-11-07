using MediatR;
using ProjectServ.Application.Models;

namespace ProjectServ.Application.MediatR.Applications.GetApplication;

public class GetApplicationCommand : IRequest<ApplicationResponse>
{
    public Guid? Id { get; set; }
}
