using ProjectServ.Domain.Entities;
using ProjectServ.Domain.Enums;
using Mapster;
using Microsoft.AspNetCore.Identity;
using ProjectServ.Application.Common;
using ProjectServ.Application.Models;

namespace ProjectServ.Application.Services;

public class UserAuthService(
    UserManager<AppUser> userManager, 
    RoleManager<AppRole> roleManager,
    JwtService jwtService)
{
    public async Task<AuthenticationResponse> CreateUser(AppUser user, string password)
    {
        Throw.UserFriendlyExceptionIf(await IsEmailAlreadyExist(user.Email!), 
            401, "User exists");

        user.Role = RoleEnum.User;
        
        var result = await userManager.CreateAsync(user, password);
        
        Throw.UserFriendlyExceptionIf(!result.Succeeded, 
            401, string.Join(" | ", result.Errors.Select(e => e.Description)));
        
        var roleExist = await roleManager.RoleExistsAsync(user.Role.ToString());
        if (!roleExist)
        {
            await roleManager.CreateAsync(new AppRole { Name = user.Role.ToString() });
        }
        
        await userManager.AddToRoleAsync(user, user.Role.ToString());
        
        var token = await jwtService.GenerateJwtToken(user);
        var response = user.Adapt<AuthenticationResponse>();
        response.Role = user.Role.ToString();
        response.Token = token;
        
        return response;
    }

    public async Task<AuthenticationResponse> SignIn(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        
        Throw.UserFriendlyExceptionIfNull(user, 
            401, "Email or password wrong");
        Throw.UserFriendlyExceptionIf(!await userManager.CheckPasswordAsync(user!, password),
            401, "Email or password wrong");
        
        var token = await jwtService.GenerateJwtToken(user!);
        var response = user.Adapt<AuthenticationResponse>();
        response.Role = user!.Role.ToString();
        response.Token = token;
        
        return response;
    }

    public async Task<bool> IsEmailAlreadyExist(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        return user is not null;
    }
}
