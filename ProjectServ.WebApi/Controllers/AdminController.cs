using LadaServ.WebApi.DTOs;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectServ.Application.MediatR.Admin.ChangeUserRole;
using ProjectServ.Application.MediatR.Admin.GetStatistics;
using ProjectServ.Application.Models;
using ProjectServ.WebApi.DTOs;
using ProjectServ.WebApi.Filters;

namespace ProjectServ.WebApi.Controllers;

[ApiKey]
[ApiController]
[AllowAnonymous]
[Route("api/admin")]
public class AdminController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet("statistics")]
    public async Task<StatisticsResponse> GetStatistics()
    {
        var command = new GetStatisticsCommand();
        return await _sender.Send(command);
    }
    
    [HttpPut("change-role")]
    public async Task RaiseUser([FromBody] ChangeRoleDto changeRoleDto)
    {
        var command = changeRoleDto.Adapt<ChangeUserRoleCommand>();
        await _sender.Send(command);
    }
}
