using ProjectServ.Application.Models;
using MediatR;

namespace ProjectServ.Application.MediatR.Applications.AcceptApplication;

public class AcceptApplicationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
