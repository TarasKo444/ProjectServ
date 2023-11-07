using ProjectServ.Domain.Enums;

namespace ProjectServ.Application.Models;

public class AuthenticationResponse
{
    public string UserName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string Token { get; set; } = null!;
}
