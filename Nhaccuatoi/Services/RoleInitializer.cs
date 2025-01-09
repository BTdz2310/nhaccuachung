using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

public static class RoleInitializer
{
    public static async Task InitializeRolesAsync(IServiceProvider serviceProvider, string[] roles)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
                Console.WriteLine($"Created role: {role}");
            }
        }
    }
}