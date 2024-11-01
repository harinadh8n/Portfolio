using ECommerceDbContext.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWebsite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure database connection
            builder.Services.AddDbContext<ECommerceContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add services to the container.
            builder.Services.AddControllersWithViews(); // Or other services as needed

            var app = builder.Build();

            // Configure middleware and routing
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers(); // Or MapRazorPages(), etc.

            app.Run();
        }
    }
}
