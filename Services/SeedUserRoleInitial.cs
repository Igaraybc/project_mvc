using Microsoft.AspNetCore.Identity;

public class SeedUserRoleInitial : ISeedUserRoleInitial
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public SeedUserRoleInitial(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    } 

    public void SeedRoles()
    {
        if(!_roleManager.RoleExistsAsync("Member").Result){
            IdentityRole role = new()
            {
                Name = "Member",
                NormalizedName = "MEMBER"
            };
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }
        if(!_roleManager.RoleExistsAsync("Admin").Result){
            IdentityRole role = new()
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            IdentityResult roleResult = _roleManager.CreateAsync(role).Result;
        }
    }

    public void SeedUser()
    {
        if(_userManager.FindByEmailAsync("usuario@localhost").Result == null){
            IdentityUser user = new(){
                UserName="usuario@localhost",
                Email= "usuario@localhost",
                NormalizedEmail = "USUARIO@LOCALHOST",
                NormalizedUserName = "USUARIO@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(user, "Member#2024").Result;

            if(result.Succeeded){
                _userManager.AddToRoleAsync(user, "Member").Wait();
            }
        }
        if(_userManager.FindByEmailAsync("admin@localhost").Result == null){
            IdentityUser user = new(){
                UserName="admin@localhost",
                Email= "admin@localhost",
                NormalizedEmail = "ADMIN@LOCALHOST",
                NormalizedUserName = "ADMIN@LOCALHOST",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            IdentityResult result = _userManager.CreateAsync(user, "Admin#2024").Result;

            if(result.Succeeded){
                _userManager.AddToRoleAsync(user, "Admin").Wait();
            }
        }
    }
}