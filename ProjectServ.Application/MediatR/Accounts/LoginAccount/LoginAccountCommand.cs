using MediatR;
using ProjectServ.Application.Models;

namespace ProjectServ.Application.MediatR.Accounts.LoginAccount;

public class LoginAccountCommand : IRequest<AuthenticationResponse>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}
