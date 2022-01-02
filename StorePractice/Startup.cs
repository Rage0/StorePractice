using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StorePractice.Models.SqlModels;
using StorePractice.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;

namespace StorePractice
{
    public class Startup
    {
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Transient
            services.AddTransient<EfProductRepository>();
            services.AddTransient<EfCategoryRepository>();
            services.AddTransient<EfOrderRepository>();
            #endregion

            #region Scoped
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddScoped<LineCategories>(lc => SessionCategory.GetCategories(lc));
            #endregion

            #region Singleton
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<MyMemoryCache<object>>();
            #endregion

            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            services.AddDbContext<ApplicationsContext>(options =>
                options.UseSqlServer
                (
                    Configuration.GetConnectionString("DefaultConnection")
                ));
            services.AddDbContext<AppIdentityContext>(options =>
                options.UseSqlServer
                (
                    Configuration.GetConnectionString("IdentityConnection")
                ));

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<AppIdentityContext>()
                .AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStatusCodePages();
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            AppIdentityContext.CreateAdminAccountAndRoles(app.ApplicationServices, Configuration).Wait();
            

            app.UseEndpoints(endpoints =>
            {
                
                endpoints.MapControllerRoute
                (
                    name: "Page",
                    pattern: "{controller=Product}/Page/{pageNow}",
                    defaults: new { Controller = "Product", action = "List" }
                );

                endpoints.MapControllerRoute
                (
                    name: "AdminPage",
                    pattern: "{controller=Admin}/{action=Product}/Page/{pageNow}",
                    defaults: new { Controller = "Admin", action = "Product" }
                );

                endpoints.MapControllerRoute
                (
                    name: "CategoriesChar",
                    pattern: "{action=Categories}/Char/{charFilter}",
                    defaults: new { Controller = "Category", action = "Categories" }
                );

                endpoints.MapControllerRoute
                (
                    name: "CategoriesChar",
                    pattern: "Account/{action=Login}",
                    defaults: new { Controller = "User", action = "Login" }
                );

                endpoints.MapControllerRoute
                (
                    name: "ProductProfile",
                    pattern: "{controller=Product}/{action=List}/{productId}",
                    defaults: new { Controller = "Product", action = "List" }
                );

                endpoints.MapControllerRoute
                (
                    name: "Product",
                    pattern: "{controller=Product}/{action=List}/{id?}"
                );
            });
            SeedData.AddData(app);
        }
    }
}
