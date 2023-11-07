using Microsoft.AspNetCore.Identity;
using ProjectServ.Domain.Entities;
using ProjectServ.Domain.Enums;

namespace ProjectServ.Infrastructure.Services;

public class DataSeeder(
    AppDbContext appDbContext, 
    RoleManager<AppRole> roleManager, 
    UserManager<AppUser> userManager)
{
    private readonly AppDbContext _appDbContext = appDbContext;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly RoleManager<AppRole> _roleManager = roleManager;
    
    public async Task Seed()
    {
        await InitRoles(); 
        await InitUserAndMaster(); 
        await InitApplications();
    }

    public async Task InitRoles()
    {
        if (!_appDbContext.Roles.Any())
        {
            await roleManager.CreateAsync(new AppRole { Name = RoleEnum.User.ToString() });
            await roleManager.CreateAsync(new AppRole { Name = RoleEnum.Master.ToString() });
        }
    }

    public async Task InitUserAndMaster()
    {
        if (!_appDbContext.Users.Any())
        {
            var user = new AppUser()
            {
                Email = "user@gmail.com",
                UserName = "user",
                Role = RoleEnum.User
            };
            
            var master = new AppUser()
            {
                Email = "master@gmail.com",
                UserName = "master",
                Role = RoleEnum.Master
            };
            
            await userManager.CreateAsync(user, "user");
            await userManager.CreateAsync(master, "master");
            
            await userManager.AddToRoleAsync(user, user.Role.ToString());
            await userManager.AddToRoleAsync(master, master.Role.ToString());
        }
    }

    public async Task InitApplications()
    {
        if (!_appDbContext.Applications.Any())
        {
            var userId = _appDbContext.Users.FirstOrDefault(u => u.Role == RoleEnum.User)!.Id;
        
            await _appDbContext.Applications.AddAsync(new()
            {
                CurrentStatus = StatusEnum.Waiting.ToString(),
                CarBrand = "TestBrand",
                CarNumber = "A12345",
                TimeOfArrival = DateTime.UtcNow.AddDays(20),
                CreatedAt = DateTime.UtcNow,
                ProblemDescription = "Test description",
                UserId = userId
            });

            await _appDbContext.SaveChangesAsync();
        }
    }
}
