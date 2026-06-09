using HRSystem.DAL.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace HRSystem.DAL.Seeding
{
    public static class AdminSeeder
    {
        public static async Task SeedAdminAsync(UserManager<HRSystemUser> userManager)
        {
            // Check if admin already exists
            var admin = await userManager.FindByEmailAsync("admin@hrsystem.com");
            if (admin != null) return;

            // Create admin user
            var aminUser = new HRSystemUser
            {
                FullName = "System Admin",
                Email = "admin@hrsystem.com",
                UserName = "admin@hrsystem.com",
                EmailConfirmed = true
            };

            var adminResult = await userManager.CreateAsync(aminUser, "Admin@123456");

            if (adminResult.Succeeded)
                await userManager.AddToRoleAsync(aminUser, "Admin");

            
        }

    }
}