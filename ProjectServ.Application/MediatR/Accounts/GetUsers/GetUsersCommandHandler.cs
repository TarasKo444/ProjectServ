using LadaServ.Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProjectServ.Application.Models;
using ProjectServ.Infrastructure;

namespace ProjectServ.Application.MediatR.Accounts.GetUsers;

public class GetUsersCommandHandler(AppDbContext appDbContext) : IRequestHandler<GetUsersCommand, IEnumerable<UserModel>>
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public async Task<IEnumerable<UserModel>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        return _appDbContext.Users.AsNoTracking().Select(u => u.Adapt<UserModel>());
    }
}
