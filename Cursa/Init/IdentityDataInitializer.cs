using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace Cursa.Init
{
    public  static class IdentityDataInitializer
    {
        public static void SeedData
        (UserManager<User> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers
            (UserManager<User> userManager)
        {
            if (userManager.FindByNameAsync
                ("admin@admin.com").Result == null)
            {
                var user = new User();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";
                user.FirstName = "Иван";
                user.MiddleName = "Иванович";
                user.LastName = "Иванов";
                user.IsPasswordChange = true;
                user.IsSystem = true;
                user.IsBanned = false;

                IdentityResult result = userManager.CreateAsync
                    (user, "Password0-").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user,
                        "Администратор").Wait();
                }
            }
        }

        public static void SeedRoles
            (RoleManager<IdentityRole<int>> roleManager)
        {
            if (!roleManager.RoleExistsAsync
                ("Администратор").Result)
            {
                var role = new IdentityRole<int>();
                role.Name = "Администратор";
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync
                ("Менеджер").Result)
            {
                var role = new IdentityRole<int>();
                role.Name = "Менеджер";
                IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
            }
        }
    }
}
