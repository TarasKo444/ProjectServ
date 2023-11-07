using MediatR;
using ProjectServ.Application.Models;

namespace ProjectServ.Application.MediatR.Applications.CloseApplication;

public class CloseApplicationCommand : IRequest<ApplicationResponse>
{
    public Guid Id { get; set; }
}
