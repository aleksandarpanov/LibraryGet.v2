using System;
using LibraryGet.DataAccess.STANDARD.Helpers;
using LibraryGet.Web.CORE.Data;
using LibraryGet.Web.CORE.Extensions;
using LibraryGet.Web.CORE.Hubs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryGet.Web.CORE
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
            services.AddLibraryGetCore();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy =>
                    policy.RequireAuthenticatedUser().RequireRole("Admin"));
            });

            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(1);
            });

            services.AddMemoryCache();

            services.AddMvc()
                .AddRazorPagesOptions(options => {
                    options.Conventions.AuthorizePage("/Books/Create", "RequireAdminRole");
                    options.Conventions.AuthorizeFolder("/Books");
                    options.Conventions.AuthorizeFolder("/BooksReservation", "RequireAdminRole");
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowCredentials();
            }));

            services.AddSignalR();

            services.AddSingleton<IUserIdProvider, NameAppUserIDProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseSession();

            // Make sure the CORS middleware is ahead of SignalR.
            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chathub");
            });

            app.UseMvc();

            serviceProvider.CreateUsersRoles().Wait();
        }
    }
}
