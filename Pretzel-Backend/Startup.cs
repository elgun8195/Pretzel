using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pretzel_Backend.DAL;
using Pretzel_Backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pretzel_Backend
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = _config.GetConnectionString("DefaultConnection");
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(op =>
            {
                op.UseSqlServer(connectionString);
            });

            services.AddSession(op =>
            {
                op.IdleTimeout=TimeSpan.FromMinutes(20);
            });

            services.AddIdentity<AppUser, IdentityRole>(op =>
            {
                op.User.RequireUniqueEmail = true;
                op.Password.RequiredLength = 6;
                op.Password.RequireNonAlphanumeric = true;
                op.Password.RequireDigit=true;
                op.Password.RequireLowercase=true;
                op.Password.RequireUppercase=true;


                op.Lockout.AllowedForNewUsers = true;
                op.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(20);
                op.Lockout.MaxFailedAccessAttempts = 3;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  "areas",
                 "{area:exists}/{controller=dashboard}/{action=Index}/{id?}"
                );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
