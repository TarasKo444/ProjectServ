using MediatR;
using ProjectServ.Application.Models;
using ProjectServ.Application.Services;

namespace ProjectServ.Application.MediatR.Accounts.LoginAccount;

public class LoginAccountCommandHandler(UserAuthService userAuthService) : IRequestHandler<LoginAccountCommand, AuthenticationResponse>
{
    public async Task<AuthenticationResponse> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
    {
        var response = await userAuthService.SignIn(request.Email!, request.Password!);
        return response;
    }
}
