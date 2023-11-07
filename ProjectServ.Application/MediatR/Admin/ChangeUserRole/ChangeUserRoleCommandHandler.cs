using ProjectServ.Domain.Enums;
using LadaServ.Infrastructure;
using MediatR;
using ProjectServ.Application.Common;
using ProjectServ.Infrastructure;

namespace ProjectServ.Application.MediatR.Admin.ChangeUserRole;

public class ChangeUserRoleCommandHandler : IRequestHandler<ChangeUserRoleCommand, Unit>
{
    private readonly AppDbContext _appDbContext;

    public ChangeUserRoleCommandHandler(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<Unit> Handle(ChangeUserRoleCommand request, CancellationToken cancellationToken)
    {
        if (!Enum.TryParse(request.Role, true, out RoleEnum newRole))
            throw new UserFriendlyException(400, "Wrong role");
        
        var user = _appDbContext.Users.FirstOrDefault(u => u.Id == request.UserId);
        
        Throw.UserFriendlyExceptionIfNull(user,
            400, "User not exist");

        user!.Role = newRole;

        _appDbContext.Update(user);
        await _appDbContext.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}
