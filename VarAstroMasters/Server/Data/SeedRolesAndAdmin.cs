using Microsoft.AspNetCore.Identity;

namespace VarAstroMasters.Server.Data;

public static class SeedRolesAndAdmin
{
    internal static async Task Seed(UserManager<User>? userManager, RoleManager<IdentityRole>? roleManager)
    {
        if (userManager == null || roleManager == null) return;
        await SeedAdminRole(roleManager);
        await SeedUserRole(roleManager);
        await SeedAdminUser(userManager);
    }

    private static async Task SeedAdminUser(UserManager<User> userManager)
    {
        var administratorUserExists = await userManager.FindByEmailAsync("admin@example.com") != null;

        if (administratorUserExists == false)
        {
            var administratorUser = new User
            {
                FirstName = "Admin",
                LastName = "Adminovič",
                UserName = "admin@example.com",
                Email = "admin@example.com"
            };

            var identityResult = await userManager.CreateAsync(administratorUser, "Kappa123!");

            if (identityResult.Succeeded) await userManager.AddToRoleAsync(administratorUser, "Administrator");
        }
    }

    private static async Task SeedAdminRole(RoleManager<IdentityRole> roleManager)
    {
        var administratorRoleExists = await roleManager.RoleExistsAsync("Administrator");
        if (administratorRoleExists == false)
        {
            var role = new IdentityRole
            {
                Name = "Administrator"
            };

            await roleManager.CreateAsync(role);
        }
    }

    private static async Task SeedUserRole(RoleManager<IdentityRole> roleManager)
    {
        var userRoleExists = await roleManager.RoleExistsAsync("User");
        if (userRoleExists == false)
        {
            var role = new IdentityRole
            {
                Name = "User"
            };

            await roleManager.CreateAsync(role);
        }
    }
}