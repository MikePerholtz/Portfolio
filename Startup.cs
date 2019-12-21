using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer;
using Portfolio.Data;
using Portfolio.Models;
using System.Text;

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
            services.AddIdentity<IdentityUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<PortfolioContext>();

            services.AddAuthentication()
        .AddCookie()
        .AddJwtBearer(cfg =>
        {
          cfg.TokenValidationParameters = new TokenValidationParameters()
          {
            ValidateIssuer = true,
            ValidIssuer = Configuration["Security:Tokens:Issuer"],
            ValidateAudience = true,
            ValidAudience = Configuration["Security:Tokens:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Security:Tokens:Key"])),

          };
        });
            
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
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                    HotModuleReplacement = true
                });
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
            if (env.IsDevelopment()) {
                app.UseStaticFiles(new StaticFileOptions()
                {
                    RequestPath = "/lib",
                    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules/"))
                });
            }

            // * mPerholtz This redirects our site to https and we get warnings for now
            //app.UseHttpsRedirection();
           
            app.UseCookiePolicy();

            //app.UseMvcWithDefaultRoute();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                        name: "Home",
                        template: "",
                        defaults: new { controller = "Home", action = "Index" });

                // routes.MapRoute(
                //         name: "ContactUsMessage",
                //         template: "",
                //         defaults: new { controller = "ContactUsMessage", action = "Index" });

                routes.MapRoute(
                        name: "default",
                        template: "_/{action}",
                        defaults: new { controller = "Home" });
            });
        }
    }
}
