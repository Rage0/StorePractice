using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StorePractice.Models.SqlModels
{
    public class AppIdentityContext: IdentityDbContext<User>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(user => user.HasProducts)
                .WithOne(product => product.User)
                .HasForeignKey(key => key.OwnerId);
            builder.Entity<User>()
                .HasMany(user => user.HasCategories)
                .WithOne(category => category.User)
                .HasForeignKey(key => key.OwnerId);
            builder.Entity<User>()
                .HasMany(user => user.HasOrders)
                .WithOne(order => order.User)
                .HasForeignKey(key => key.OwnerId);

            base.OnModelCreating(builder);
        }
    }
}
