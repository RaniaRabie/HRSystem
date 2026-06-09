using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRSystem.DAL.Seeding
{
    //public class HRSeeder
    //{
    //    public static async Task SeedHRAsync(UserManager<HRSystemUser> userManager)
    //    {
    //        // Check if admin already exists
    //        var hr = await userManager.FindByEmailAsync("mgrgrg@gmail.com");
    //        if (hr != null) return;

    //        // Create hr user
    //        var hrUser = new HRSystemUser
    //        {
    //            FullName = "Rania Rabie",
    //            Email = "mgrgmrg@gmail.com",
    //            UserName = "rania@hruser.com",
    //            EmailConfirmed = true
    //        };

    //        var hrResult = await userManager.CreateAsync(hrUser, "Rania@123456");

    //        if (hrResult.Succeeded)
    //            await userManager.AddToRoleAsync(hrUser, "HR");


    //    }
    //}
}
