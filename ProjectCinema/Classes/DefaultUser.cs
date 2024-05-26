using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectCinema.Data;
using ProjectCinema.Models;
using ProjectCinema.Enums;

namespace ProjectCinema.Classes
{
    public static class DefaultUser
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
                context.Database.EnsureCreated();
                if (await userManager.FindByNameAsync("admin") == null)
                {
                    var admin = new User
                    {
                        UserName = "admin",
                        Email = "admin@example.com",
                        Name = "Admin",
                        Role = Roles.Admin
                    };
                    var result = await userManager.CreateAsync(admin, "Admin12345");
                }
            }
        }
    }
}
