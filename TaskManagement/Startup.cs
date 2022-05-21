using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagement
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
            services.AddDbContext<MyContext>(opts =>
            {
                opts.UseSqlServer(Configuration["ConnectionStrings:DefaultConnectionString"]);
            });
            services.AddControllersWithViews();
            services.AddIdentity<AppUser, AppRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opts.Password.RequiredLength = 4;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<MyContext>().AddDefaultTokenProviders();

            CookieBuilder cookieBuilder = new CookieBuilder();
            cookieBuilder.Name = "MyBlog";
            cookieBuilder.HttpOnly = false;
            cookieBuilder.SameSite = SameSiteMode.Lax; //cookie kaydoldu�u zaman sadece sistem �zerinden eri�mesini sa�l�yorum
            cookieBuilder.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Home/Login"); //k�s�tl� sayafaya eri�meye �al���rsa logine g�nder (�ye olmam��)
                options.LogoutPath = new PathString("/Home/LogOut");//logout i�lemi ger�ekle�tiriyor
                options.Cookie = cookieBuilder;
                options.SlidingExpiration = true;     //cookinin �mr� bittikten sonra tekar kullan�c� istek atarsa �mr� uzat�r
                options.ExpireTimeSpan = TimeSpan.FromDays(60);     //60 g�n boyunca sitemde gezebilir
                options.AccessDeniedPath = new PathString("/Member/AccessDenied");  //e�er kullan�c�(�ye olmu�) o sayfaya eri�im izni yoksa eri�emez
            });
            services.AddMvc();
            services.AddRazorPages();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
