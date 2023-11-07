using LadaServ.WebApi.DTOs;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectServ.Application.MediatR.Accounts.GetUsers;
using ProjectServ.Application.MediatR.Accounts.LoginAccount;
using ProjectServ.Application.MediatR.Accounts.RegisterAccount;
using ProjectServ.Application.Models;
using ProjectServ.WebApi.DTOs;

namespace ProjectServ.WebApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/accounts")]
public class AccountController(
        ISender mediator)
    : ControllerBase
{
    private readonly ISender _mediator = mediator;

    [HttpPost("register")]
    public async Task<AuthenticationResponse> Register([FromBody] RegisterDto registerDto)
    {
        var command = registerDto.Adapt<RegisterAccountCommand>();
        return await _mediator.Send(command);
    } 

    [HttpPost("login")]
    public async Task<AuthenticationResponse> Login([FromBody] LoginDto registerDto)
    {
        var command = registerDto.Adapt<LoginAccountCommand>();
        return await _mediator.Send(command);
    }

    [HttpGet]
    public async Task<IEnumerable<UserModel>> GetUsers()
    {
        var command = new GetUsersCommand();
        return await _mediator.Send(command);
    }
}
