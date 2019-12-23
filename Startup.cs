﻿using System;
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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer;
using Portfolio.Data;
using Portfolio.Models;
using System.Text;
using Microsoft.Extensions.Hosting;
//using Microsoft.AspNetCore.SpaServices.VueCli;
//using Microsoft.AspNetCore.SpaServices.Webpack;

namespace Portfolio
{
    public class Startup
    {
        

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddDbContext<PortfolioContext>(conf =>
            {
                var connection = Configuration.GetConnectionString("BabylonSystemDb");
                conf.UseSqlServer(connection, opt => opt.EnableRetryOnFailure());
            });

            services.AddIdentity<IdentityUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            });
            

            services.AddAuthentication()
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
            

            services.AddMvc(option => 
                option.EnableEndpointRouting = false
            );
                    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // mPerholtz obsolete in favor of Microsoft.AspNetCore.SpaServices.Extensions 
                // app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                //     HotModuleReplacement = true;
                // });
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
                app.UseDeveloperExceptionPage();
                
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

            // app.UseSpa(spa => {
            //     spa.Options.SourcePath = "VueApp";

            //     if (env.IsDevelopment())
            //     {
            //         spa.UseVueCliServer(npmScript: "serve");
            //     }
            // });
        }
    }
}
