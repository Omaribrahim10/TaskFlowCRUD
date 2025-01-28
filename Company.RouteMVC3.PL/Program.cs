using Company.RouteMVC3.BL;
using Company.RouteMVC3.BL.Interfaces;
using Company.RouteMVC3.BL.Repositories;
using Company.RouteMVC3.DAL.Data.Contexts;
using Company.RouteMVC3.DAL.Models;
using Company.RouteMVC3.PL.Mapping.Employees;
using Company.RouteMVC3.PL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Company.RouteMVC3.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //builder.Services.AddScoped<AppDbContext>(); // Allow Dependency Injection for AppDbContext

            //builder.Services.AddDbContext<AppDbContext>(options => 
            //{
            //	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            //}
            //); 

            builder.Services.AddDbContext<AppDbContext>(
                            options =>
                                {
                                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                                },
                                ServiceLifetime.Transient 
                                );



            // Allow Dependency Injection for AppDbContext

            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>(); // Allow Dependency Injection for Department Repository
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>(); // Allow Dependency Injection for Department Repository


            //builder.Services.AddScoped(); // Per Request , Unreachable Object
            //builder.Services.AddSingleton(); // Per App
            //builder.Services.AddTransient(); // Per Operations

            //builder.Services.AddDbContext<AppDbContext>(ServiceLifetime.Transient);


            builder.Services.AddScoped<IScopedService, ScopedService>();
            builder.Services.AddTransient<ITransientService, TransientService>();
            builder.Services.AddSingleton<ISingltonService, SingltonService>();

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(EmployeeProfile));

            //builder.Services.AddScoped<UserManager<ApplicationUser>>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
                );

            app.Run();
        }
    }
}
