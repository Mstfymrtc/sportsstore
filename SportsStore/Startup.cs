﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsStore.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace SportsStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }
        //
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));

            #region lines for identity

            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreIdentity:ConnectionString"]));
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders(); 
            #endregion


            services.AddMvc(); ////n
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddTransient<IOrderRepository, EFOrderRepository>();

            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            /*IMPORTANTIMPORTANTIMPORTANTIMPOTANT*/
            /*IMPORTANTIMPORTANTIMPORTANTIMPOTANT*/
            /*IMPORTANTIMPORTANTIMPORTANTIMPOTANT*/
            /*IMPORTANTIMPORTANTIMPORTANTIMPOTANT*/
            /////////////////////////////* session data is lost when the application is stopped or restarted. */////////////////
            /*IMPORTANTIMPORTANTIMPORTANTIMPOTANT*/
            /*IMPORTANTIMPORTANTIMPORTANTIMPOTANT*/
            /*IMPORTANTIMPORTANTIMPORTANTIMPOTANT*/
            /*IMPORTANTIMPORTANTIMPORTANTIMPOTANT*/
            services.AddMemoryCache();
            services.AddSession();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseDeveloperExceptionPage(); /////n
            app.UseStatusCodePages();         //////n
            app.UseStaticFiles();             //////n
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {          ///////n
                routes.MapRoute(
                     name: null,
                     template: "{category}/Page{productPage:int}",
                     defaults: new { controller = "Product", action = "List" }
                     );

                routes.MapRoute(
                     name: null,
                     template: "Page{productPage:int}",
                     defaults: new
                     {
                         controller = "Product",
                         action = "List",
                         productPage = 1
                     });

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    });

                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new
                    {
                        controller = "Product",
                        action = "List",
                        productPage = 1
                    });

                routes.MapRoute(
                    name: null,
                    template: "{controller}/{action}/{id?}");
            });




            SeedData.EnsurePopulated(app);
            IdentitySeedData.EnsurePopulated(app); // identity seed data!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
