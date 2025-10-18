using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MonthlyClaimSystem_PartTwo.Data;
namespace MonthlyClaimSystem_PartTwo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<MonthlyClaimSystem_PartTwoContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MonthlyClaimSystem_PartTwoContext") ?? throw new InvalidOperationException("Connection string 'MonthlyClaimSystem_PartTwoContext' not found.")));

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
