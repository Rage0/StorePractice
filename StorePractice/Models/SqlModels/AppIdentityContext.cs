using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace StorePractice.Models.SqlModels
{
    public class AppIdentityContext: IdentityDbContext<User>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {
            /*Database.EnsureDeleted();*/
            Database.EnsureCreated();
            
        }

        public static async Task CreateAdminAccountAndRoles(IServiceProvider service, IConfiguration configuration)
        {
            UserManager<User> userManager = service.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            string userName = configuration["Data:AdminUser:Name"];
            string email = configuration["Data:AdminUser:Email"];
            string password = configuration["Data:AdminUser:Password"];

            foreach (string roleName in configuration.GetSection("Data:Roles").Get<string[]>())
            {
                if (await roleManager.FindByNameAsync(roleName) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            if (await userManager.FindByNameAsync(userName) == null)
            {
                User user = new User
                {
                    UserName = userName,
                    Email = email,
                };

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded && await roleManager.FindByNameAsync("Admin") != null)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(user => user.HasProducts)
                .WithOne()
                .HasForeignKey(key => key.OwnerId);
            builder.Entity<User>()
                .HasMany(user => user.HasCategories)
                .WithOne()
                .HasForeignKey(key => key.OwnerId);
            builder.Entity<User>()
                .HasMany(user => user.HasOrders)
                .WithOne()
                .HasForeignKey(key => key.OwnerId);

            base.OnModelCreating(builder);
        }
    }
}
