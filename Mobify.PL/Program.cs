using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mobify.BLL.AutoMapper;
using Mobify.BLL.SeedingData;
using Mobify.BLL.Services.Abstraction;
using Mobify.BLL.Services.Implmentation;
using Mobify.DAL.DataBase.DBContext;
using Mobify.DAL.Entities;
using Mobify.DAL.Repo.Abstraction;
using Mobify.DAL.Repo.Implmentation;

namespace Mobify.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDBContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });
            builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
            builder.Services.AddScoped<ICategoryService, CategoryServices>();
            builder.Services.AddScoped<IBrandRepo, BrandRepo>();
            builder.Services.AddScoped<IBrandServices, BrandServices>();
            builder.Services.AddScoped<IProductServices, ProductServices>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
            builder.Services.AddScoped<IHomePageServices, HomePageServices>();
            builder.Services.AddScoped<IProductDetailsService, ProductDetailsService>();
            builder.Services.AddScoped<IAccountServices,AccountServices>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(op =>
            {
                op.Password.RequiredLength = 4;
                op.Password.RequireUppercase = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDBContext>();
             var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

                await SeedingData.SeedRoles(roleManager);

                await SeedingData.SeedAdmin(userManager);
            }
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
