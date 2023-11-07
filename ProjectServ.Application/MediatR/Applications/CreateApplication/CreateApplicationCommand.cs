using ProjectServ.Application.Models;
using MediatR;

namespace ProjectServ.Application.MediatR.Applications.CreateApplication;

public class CreateApplicationCommand : IRequest<Unit>
{
    public string? CarNumber { get; set; }
    public string? CarBrand { get; set; }
    public DateTime? TimeOfArrival { get; set; }
    public string? ProblemDescription { get; set; }
}
