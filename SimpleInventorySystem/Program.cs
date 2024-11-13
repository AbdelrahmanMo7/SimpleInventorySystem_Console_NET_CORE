using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleInventorySystem.BusinessLogic;
using SimpleInventorySystem.DataAccesslogic;
using SimpleInventorySystem.DataLayer;
using SimpleInventorySystem.DataLayer.Models;
using SimpleInventorySystem.Display;
using SimpleInventorySystem.Interfaces;





namespace SimpleInventorySystem
{
    internal class Program
    {
        static async Task Main(string[] args)
        {

            //----add services Registeration ---
           var builder = Host.CreateDefaultBuilder(args)

                .ConfigureLogging((hostContext, logging) =>
                {
                    // Hide 'Information' level logs 
                    logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning); 
                })
                .ConfigureServices((hostContext, services) =>
                {
                   // Configure DbContext with a connection string
                   services.AddDbContext<InventoryContext>(options =>
                       options.UseSqlServer("Server=.;Database=SimpleInventorySystem;Trusted_Connection=True;Encrypt=true;TrustServerCertificate=yes"));
                   services.AddTransient<IUserInteraction, UserInteraction>();
                   services.AddScoped<IInventoryManagement, InventoryManagement>();
                   services.AddScoped<IProductRepository, ProductRepository>();
                   services.AddScoped<ICategoryRepository, CategoryRepository>();

                   services.AddScoped(typeof(IRepository<Product>), typeof(Repository<Product>));
                   services.AddScoped(typeof(IRepository<Category>), typeof(Repository<Category>));
                });

            var host = builder.Build();

            var userInteraction = host.Services.GetRequiredService<IUserInteraction>();

            await userInteraction.InteractWithUserAsync();

            await host.RunAsync();
        }
    }
}