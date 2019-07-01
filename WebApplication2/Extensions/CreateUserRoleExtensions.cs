using LibraryGet.Web.CORE.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace LibraryGet.Web.CORE.Extensions
{
    public static class CreateUserRoleExtensions
    {
        public static async Task CreateUsersRoles(this IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] rolesNames = { "Admin", "User" };

            IdentityResult roleResult;

            foreach (var roleName in rolesNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            ApplicationUser user = await UserManager.FindByEmailAsync("aleksandar.panov@gmail.com");

            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = "aleksandar.panov@gmail.com",
                    Email = "aleksandar.panov@gmail.com",
                };
                await UserManager.CreateAsync(user, "Test@123");
            }
            await UserManager.AddToRoleAsync(user, "Admin");

        }
    }
}
