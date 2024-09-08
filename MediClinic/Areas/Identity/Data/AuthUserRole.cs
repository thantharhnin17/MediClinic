using MediClinic.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace MediClinic.Areas.Identity.Data
{
    public static class AuthUserRole
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetService<UserManager<AuthUser>>();

            if (roleManager == null || userManager == null)
            {
                throw new InvalidOperationException("RoleManager or UserManager service is not registered.");
            }

            // Seed Roles
            if (!await roleManager.RoleExistsAsync(Roles.Admin.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            }

            if (!await roleManager.RoleExistsAsync(Roles.User.ToString()))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));
            }

            // Create admin user
            var adminEmail = "admin@gmail.com";
            var adminPassword = "Admin@123";

            var adminUser = new AuthUser
            {
                UserName = "admin@gmail.com",
                Email = adminEmail,
                EmailConfirmed = true
            };

            var userInDb = await userManager.FindByEmailAsync(adminEmail);
            if (userInDb == null)
            {
                var result = await userManager.CreateAsync(adminUser, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, Roles.Admin.ToString());
                }
                else
                {
                    throw new InvalidOperationException("Failed to create admin user: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }
    }

}
