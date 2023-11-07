using ProjectServ.Domain.Entities;
using Mapster;
using MediatR;
using ProjectServ.Application.Models;
using ProjectServ.Application.Services;

namespace ProjectServ.Application.MediatR.Accounts.RegisterAccount;

public class RegisterAccountCommandHandler(
    UserAuthService userAuthService)
    : IRequestHandler<RegisterAccountCommand, AuthenticationResponse>
{
    public async Task<AuthenticationResponse> Handle(RegisterAccountCommand request, CancellationToken cancellationToken)
    {
        request.Password = request.Password!.Trim();
        request.Email = request.Email!.Trim();
        request.UserName = request.UserName!.Trim();
        
        var user = request.Adapt<AppUser>();

        var response = await userAuthService.CreateUser(user, request.Password!);
        
        return response;
    }
}
