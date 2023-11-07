using MediatR;

namespace ProjectServ.Application.MediatR.Admin.ChangeUserRole;

public class ChangeUserRoleCommand : IRequest<Unit>
{
    public Guid? UserId { get; set; }
    public string? Role { get; set; } = null!;
}
