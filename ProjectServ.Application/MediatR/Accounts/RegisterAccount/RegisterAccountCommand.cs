using MediatR;
using ProjectServ.Application.Models;

namespace ProjectServ.Application.MediatR.Accounts.RegisterAccount;

public class RegisterAccountCommand : IRequest<AuthenticationResponse>
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}
