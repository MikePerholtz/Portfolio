using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.SqlServer;
using Portfolio.Data;
using Portfolio.Models;



namespace Portfolio
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            var connection = Configuration.GetConnectionString("BabylonSystemDb");
            services.AddDbContext<PortfolioContext>(options => options.UseSqlServer(connection));

            services.AddMvc();
                    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            // mPerholtz See Wildermuth Vue Js Course > Getting Started > Where We're Starting
            // This is a trick to reroute lib folder requests to node_modules folder
            // if (env.IsDevelopment()) {
            //     app.UseStaticFiles(new StaticFileOptions()
            //     {
            //         RequestPath = "/lib",
            //         FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules/"))
            //     });
            // }

            // * mPerholtz This redirects our site to https and we get warnings for now
            //app.UseHttpsRedirection();
           
            app.UseCookiePolicy();

            //app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                        name: "ContactUsMessage",
                        template: "",
                        defaults: new { controller = "ContactUsMessage", action = "Index" });
                // routes.MapRoute(
                //         name: "Create",
                //         template: "",
                //         defaults: new { controller = "ContactUsMessage", action = "Create" });

                routes.MapRoute(
                        name: "default",
                        template: "_/{action}",
                        defaults: new { controller = "ContactUsMessage" });
            });
        }
    }
}
