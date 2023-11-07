using System.Security.Claims;
using ProjectServ.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace ProjectServ.Application.Services;

public class UserAccessorService(IHttpContextAccessor contextAccessor)
{
    private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

    public Guid UserId => Guid.Parse(_contextAccessor.HttpContext.User
        .FindFirst(ClaimTypes.NameIdentifier)!.Value);

    public RoleEnum Role => Enum.Parse<RoleEnum>(_contextAccessor.HttpContext.User
        .FindFirst(ClaimTypes.Role)!.Value, true);
}
