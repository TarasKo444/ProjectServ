using LadaServ.WebApi.DTOs;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectServ.Application.MediatR.Applications.CreateApplication;
using ProjectServ.Application.MediatR.Applications.GetApplication;
using ProjectServ.Application.MediatR.Applications.GetApplications;
using ProjectServ.Application.Models;
using ProjectServ.Domain.Enums;

namespace ProjectServ.WebApi.Controllers;

[ApiController]
[Route("api/user/applications")]
[Authorize(Roles = nameof(RoleEnum.User))]
public class UserApplicationController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IEnumerable<ApplicationResponse>> GetUserApplications()
    {
        var command = new GetApplicationsCommand();
        return await _sender.Send(command);
    }

    [HttpGet("{id:guid}")]
    public async Task<ApplicationResponse> GetUserApplication(Guid id)
    {
        var command = new GetApplicationCommand
        {
            Id = id
        };
        
        return await _sender.Send(command);
    }
    
    [HttpPost]
    public async Task AddUserApplication([FromBody] ApplicationDto applicationDto)
    {
        var command = applicationDto.Adapt<CreateApplicationCommand>();
        await _sender.Send(command);
    }
}
