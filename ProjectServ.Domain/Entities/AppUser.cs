using Microsoft.AspNetCore.Identity;
using ProjectServ.Domain.Enums;

namespace ProjectServ.Domain.Entities;

public class AppUser : IdentityUser<Guid>
{
    public RoleEnum Role { get; set; }
    
    public ICollection<Application>? UserApplications { get; set; }
    public ICollection<Application>? MasterApplications { get; set; }
}
