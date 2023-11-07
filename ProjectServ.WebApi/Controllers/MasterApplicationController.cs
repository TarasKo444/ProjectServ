using ProjectServ.Application.MediatR.Applications.AcceptApplication;
using ProjectServ.Application.MediatR.Applications.CloseApplication;
using ProjectServ.Application.MediatR.Applications.GetApplication;
using ProjectServ.Application.MediatR.Applications.GetApplications;
using ProjectServ.Application.MediatR.Applications.GetApplicationsByStatus;
using ProjectServ.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectServ.Domain.Enums;

namespace LadaServ.WebApi.Controllers;

[ApiController]
[Route("api/master/applications")]
[Authorize(Roles = nameof(RoleEnum.Master))]
public class MasterApplicationController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IEnumerable<ApplicationResponse>> GetApplications()
    {
        var command = new GetApplicationsCommand();
        return await _sender.Send(command);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ApplicationResponse> GetApplication(Guid id)
    {
        var command = new GetApplicationCommand
        {
            Id = id
        };
        
        return await _sender.Send(command);
    }
    
    [HttpGet("{status:alpha?}")]
    public async Task<IEnumerable<ApplicationResponse>> GetApplicationsByStatus(string status)
    {
        var command = new GetApplicationsByStatusCommand
        {
            Status = status
        };
        
        return await _sender.Send(command);
    }
    
    [HttpPost("accept/{id:guid}")]
    public async Task AcceptApplication(Guid id)
    {
        var command = new AcceptApplicationCommand
        {
            Id = id
        };
        
        await _sender.Send(command);
    }
    
    [HttpPost("close/{id:guid}")]
    public async Task<ApplicationResponse> CloseApplication(Guid id)
    {
        var command = new CloseApplicationCommand
        {
            Id = id
        };
        
        return await _sender.Send(command);
    }
}
