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

namespace StorePractice
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Transient
            services.AddTransient<IProductRepository, EfProductRepository>();
            services.AddTransient<ICategoryRepository, EfCategoryRepository>();
            services.AddTransient<IOrderRepository, EfOrderRepository>();
            #endregion

            #region Scoped
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddScoped<LineCategories>(lc => SessionCategory.GetCategories(lc));
            services.AddScoped<ProductInteraction>(sprod => SessionProduct.GetSessionProduct(sprod));
            #endregion

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
            services.AddDbContext<ApplicationsContext>(options =>
                options.UseSqlServer
                (
                    Configuration.GetConnectionString("DefaultConnection")
                ));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
