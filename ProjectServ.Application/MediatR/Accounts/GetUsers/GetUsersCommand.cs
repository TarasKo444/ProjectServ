using MediatR;
using ProjectServ.Application.Models;

namespace ProjectServ.Application.MediatR.Accounts.GetUsers;

public class GetUsersCommand : IRequest<IEnumerable<UserModel>>
{
    
}
