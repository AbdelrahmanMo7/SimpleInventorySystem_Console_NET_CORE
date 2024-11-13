using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SimpleInventorySystem.BusinessLogic;
using SimpleInventorySystem.DataAccesslogic;
using SimpleInventorySystem.DataLayer;
using SimpleInventorySystem.DataLayer.Models;
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
                    // Suppress EF Core query execution logs (set to 'Warning' to hide 'Information' logs)
                    logging.AddFilter("Microsoft.EntityFrameworkCore.Database.Command", LogLevel.Warning); // Hide 'Information' level logs from EF Core queries

                    //// Optional: Suppress all 'Information' level logs globally, or adjust as needed
                    //logging.AddFilter("Microsoft", LogLevel.Warning);  // Hide 'Information' logs from other Microsoft-related logs, like System.Data, etc.

                    //// Optional: Add a filter for your application if you don't want certain categories to log 'Information'
                    //logging.AddFilter("SimpleInventorySystem", LogLevel.Warning);  // Hide 'Information' logs for your app's category (if needed)

                    //// Add console logging to output remaining logs to the console
                    //logging.AddConsole();
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

        //   var serviceProvider = new ServiceCollection()
         //.AddDbContext<InventoryContext>()
         //.AddScoped<IUserInteraction, UserInteraction>()
         //.AddScoped<IInventoryManagement, InventoryMansgement>()
         //.AddScoped<IProductRepository, ProductRepository>()
         //.AddScoped<ICategoryRepository, CategoryRepository>()
         //.AddScoped(typeof(IRepository<Product>), typeof(Repository<Product>))
         //.AddScoped(typeof(IRepository<Category>), typeof(Repository<Category>))
         //.BuildServiceProvider();

         //   var userInteraction = serviceProvider.GetService<IUserInteraction>();




            var userInteraction = host.Services.GetRequiredService<IUserInteraction>();


            // make user interact with the system via the console
            bool IsSystemOpen_ = true;
            Console.Write("\r\n                   <<<  Inventory system is running...  >>>\r\n");

            while (IsSystemOpen_)
            {
                await userInteraction.DisplaySystemOptionsAsync();
                string option_Num = Console.ReadLine();

                switch (option_Num)
                {
                    //1- Prompt the user to enter new product details
                    case "1":
                        await userInteraction.AddingNewProductAsync();
                        break;

                    //2- Prompt the user to enter new category details
                    case "2":
                        await userInteraction.AddingNewCategoryAsync();
                        break;

                    //3-  Prompt the user to enter product name and new Stock Quantity for updating
                    case "3":
                        await userInteraction.UpdatingStockQuantityAsync();
                        break;

                    //4- List all Products in a particular Category
                    case "4":
                        await userInteraction.DisplayProductsByCategoryAsync();
                        break;

                    //5- List all Categories
                    case "5":
                        await userInteraction.DisplayCategoriesAsync();
                        break;

                    //6- Calculate and display the total inventory value 
                    case "6":
                        await userInteraction.DisplayTotalInventoryAsync();
                        break;

                    //Close the System.
                    case "7":
                        //change the value of isSystemOpen? to false to stop the loop
                        Console.WriteLine("^^^  System is closing. Goodby.  ^^^");
                        IsSystemOpen_ = false;
                        break;

                    default:
                        Console.WriteLine("+++ incorrect Option Number !!!  please enter correct number of (1,2,3,4,5)..... ");
                        break;
                }

                Console.WriteLine("Press on any keyboard Key to continue [Enter] ...........");
                Console.ReadLine();
            
            }

            await host.RunAsync();
        }
    }
}