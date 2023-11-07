namespace ProjectServ.WebApi.DTOs;

public class ChangeRoleDto
{
    public Guid? UserId { get; set; }
    public string? Role { get; set; }
}
