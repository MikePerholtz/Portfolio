using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Portfolio.Data;
using System.Text;
using Microsoft.Extensions.Hosting;
using VueCliMiddleware;
using Portfolio.Config;
using System.Data.SqlClient;
//using Microsoft.AspNetCore.SpaServices.VueCli;
//using Microsoft.AspNetCore.SpaServices.Webpack;

namespace Portfolio
{
    public class Startup
    {
        private string portfolioDbPaxxword = null;
        private string _connection = null;
        readonly IWebHostEnvironment HostingEnvironment;        
        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env)
        {

            HostingEnvironment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", 
                            optional: false, 
                            reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
            
            

        }       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllersWithViews();

            // Add AddRazorPages if the app uses Razor Pages.
            services.AddRazorPages();

           _connection = BuildDbConnectionString(Configuration);

            services.AddDbContext<PortfolioContext>(conf =>
            {
                conf.UseSqlServer(_connection, opt => opt.EnableRetryOnFailure());
            });
            
            
            #region Strongly Typed Config Idea
            // mPerholtz => see Rick Strahl's Album Viewer:
            //Also make top level configuration available (for EF configuration and access to connection string)

            // var config = new ApplicationConfiguration();
            // Configuration.Bind("Application", config);
            // services.AddSingleton(config);

            // App.Configuration = config;
            //services.AddSingleton<IConfigurationRoot>(Configuration);
            #endregion Strongly Typed Config Idea
            
            services.AddSingleton<IConfiguration>(Configuration);

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
            
            // In production, the Vue files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            #region mPerholtz commented out for Vue CLI 4 and .Net Core 3
            // services.AddMvc(option => 
            //     option.EnableEndpointRouting = false
            // );
            #endregion
                    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Write the SQL Connectionstring to the browser for Debug/Troubleshooting
            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync($"DB Connection: {_connection}");
            // });
            #endregion

            if (env.IsDevelopment())
            {
                // mPerholtz obsolete in favor of Microsoft.AspNetCore.SpaServices.Extensions 
                // app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions {
                //     HotModuleReplacement = true;
                // });
                app.UseDeveloperExceptionPage();
                
                #region This is a trick to reroute lib folder requests to node_modules folder
                
                // mPerholtz See Wildermuth Vue Js Course > Getting Started > Where We're Starting
                // This is a trick to reroute lib folder requests to node_modules folder

                // app.UseStaticFiles(new StaticFileOptions()
                // {
                //     RequestPath = "/lib",
                //     FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules/"))
                // });
                #endregion This is a trick to reroute lib folder requests to node_modules folder
                
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            // * mPerholtz This redirects our site to https and we get warnings for now
            //app.UseHttpsRedirection();
           
            app.UseCookiePolicy();

            //app.UseMvcWithDefaultRoute();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");

                if (env.IsDevelopment())
                {
                    endpoints.MapToVueCliProxy(
                        "{*path}",
                        new SpaOptions { SourcePath = "ClientApp" },
                        npmScript: "serve",
                        regex: "Compiled successfully");
                }

                // Add MapRazorPages if the app uses Razor Pages. Since Endpoint Routing includes support for many frameworks, adding Razor Pages is now opt -in.
                endpoints.MapRazorPages();
            });

            #region UseMvc routes section commented out
            // app.UseMvc(routes =>
            // {
            //     routes.MapRoute(
            //             name: "Home",
            //             template: "",
            //             defaults: new { controller = "Home", action = "Index" });

            //     // routes.MapRoute(
            //     //         name: "ContactUsMessage",
            //     //         template: "",
            //     //         defaults: new { controller = "ContactUsMessage", action = "Index" });

            //     routes.MapRoute(
            //             name: "default",
            //             template: "_/{action}",
            //             defaults: new { controller = "Home" });
            // });
            #endregion

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
            });

            #region app.UseSpa pre upgrade to Vue CLI 4 and .net core 3
            // app.UseSpa(spa => {
            //     spa.Options.SourcePath = "VueApp";

            //     if (env.IsDevelopment())
            //     {
            //         spa.UseVueCliServer(npmScript: "serve");
            //     }
            // });
            #endregion
        }     

        /// <summary>
        /// mPerholtz if this is dev mode we can tap into the local MS Secret Store to grab
        /// db connection strings, passwords, etc. (see SM Secret Store in the ReadMe.md for more details)
        /// </summary>
        private static string BuildDbConnectionString(IConfiguration _config) 
        {
            
            var dbSettings = _config.GetSection("Sql:Database")        //Get the values from local secret store .json file... 
                                    .Get<DbSettings>() as DbSettings;  //And Assign them to our strongly typed POCO 

            var builder = new SqlConnectionStringBuilder(dbSettings.ConnectionString);
            builder.Password = dbSettings.DbPaxxword;

            return builder.ConnectionString;

        }     
    }


    
}
