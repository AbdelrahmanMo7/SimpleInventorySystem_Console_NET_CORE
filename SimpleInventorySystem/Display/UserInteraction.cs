using SimpleInventorySystem.DataLayer.Models;
using SimpleInventorySystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventorySystem.Display
{
    // class to handeing the user interacting
    public class UserInteraction : IUserInteraction
    {

        private readonly IInventoryManagement inventoryManagement;

        public UserInteraction(IInventoryManagement _inventoryManagement)
        {
            inventoryManagement = _inventoryManagement;

        }

        public async Task InteractWithUserAsync()
        {

            // make user interact with the system via the console
            bool IsSystemOpen_ = true;
            Console.Write("\r\n                   <<<  Inventory system is running...  >>>\r\n");

            while (IsSystemOpen_)
            {
                await this.DisplaySystemOptionsAsync();
                string option_Num = Console.ReadLine();

                switch (option_Num)
                {
                    //1- Prompt the user to enter new product details
                    case "1":
                        await this.AddingNewProductAsync();
                        break;

                    //2- Prompt the user to enter new category details
                    case "2":
                        await this.AddingNewCategoryAsync();
                        break;

                    //3-  Prompt the user to enter product name and new Stock Quantity for updating
                    case "3":
                        await this.UpdatingStockQuantityAsync();
                        break;

                    //4- List all Products in a particular Category
                    case "4":
                        await this.DisplayProductsByCategoryAsync();
                        break;

                    //5- List all Categories
                    case "5":
                        await this.DisplayCategoriesAsync();
                        break;

                    //6- Calculate and display the total inventory value 
                    case "6":
                        await this.DisplayTotalInventoryAsync();
                        break;

                    //Close the System.
                    case "7":
                        //change the value of isSystemOpen? to false to stop the loop
                        Console.WriteLine("^^^  System is closing. Goodby.  ^^^");
                        IsSystemOpen_ = false;
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("+++ incorrect Option Number !!!  please enter correct number of (1,2,3,4,5,6,7)  ..... ");
                        break;
                }

              
                Console.WriteLine("Press on any keyboard Key to continue [Enter] ...........");
                Console.ReadLine();

            }
        }

        // display system options to user
        public async Task DisplaySystemOptionsAsync()
        {
            Console.WriteLine(" ---->>  System options :  \r\n" +
                  " 1 - Add New Product. \r\n" +
                  " 2 - Add New category. \r\n" +
                  " 3 - Update Stock Quantity. \r\n" +
                  " 4 - List all Products in a particular Category.\r\n" +
                  " 5 - List all Categories.\r\n" +
                  " 6 - Display Total Inventory Value. \r\n" +
                  " 7 - Close the System. \r\n" +
                  "  < Select Number (1,2,3,4,5,6,7) > : ");

        }

        //1- Prompt the user to enter new product details
        public async Task AddingNewProductAsync()
        {
            bool result = true;
            string name = "";
            do
            {
                Console.Write("- Enter new Product Name : ");
                name = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrEmpty(name))
                {
                    result = true;
                }
                else
                {
                    result = false;
                    Console.WriteLine("++ incorrect input format please Enter name correctely !!! ");
                }
            }
            while (!result);


            int category_index = -1;
            await DisplayCategoriesAsync();
            do
            {
                Console.Write("- Enter Category number from (Categories List) : ");
                result = int.TryParse(Console.ReadLine(), out int id);
                if (result)
                {
                    category_index = id;
                }
                else
                {
                    Console.WriteLine("++ incorrect input format please Enter numbers !!! ");
                }
            }
            while (!result);

            double price = 0.0;
            do
            {
                Console.Write("- Enter Price : ");
                result = double.TryParse(Console.ReadLine(), out double priceResult);
                if (result)
                {
                    price = priceResult;
                }
                else
                {
                    Console.WriteLine("++ incorrect input format please Enter numbers !!! ");
                }
            }
            while (!result);

            int stockQuantity = 0;
            do
            {
                Console.Write("- Enter Stock Quantity : ");
                result = int.TryParse(Console.ReadLine(), out int Quantity);
                if (result)
                {
                    stockQuantity = Quantity;
                }
                else
                {
                    Console.WriteLine("++ incorrect input format please Enter numbers !!! ");
                }
            }
            while (!result);
            // add new product
            await inventoryManagement.AddNewProductAsync(new Product
            {
                Product_Name = name,
                Category_ID = category_index - 1,
                Price = price,
                StockQuantity = stockQuantity
            });
        }

        //2- Prompt the user to enter new category details
        public async Task AddingNewCategoryAsync()
        {
            bool result = true;
            string name = "";
            do
            {
                Console.Write("- Enter new Category Name : ");
                name = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrEmpty(name))
                {
                    result = true;
                }
                else
                {
                    result = false;
                    Console.WriteLine("++ incorrect input format please Enter name correctely !!! ");
                }
            }
            while (!result);

            await inventoryManagement.AddNewCategoryAsync(name);

        }


        //3-  Prompt the user to enter product name and new Stock Quantity for updating
        public async Task UpdatingStockQuantityAsync()
        {
            bool result = true;
            string product_name = "";
            do
            {
                Console.Write("- Enter new Product Name : ");
                product_name = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(product_name) || !string.IsNullOrEmpty(product_name))
                {
                    result = true;
                }
                else
                {
                    result = false;
                    Console.WriteLine("++ incorrect input format please Enter name correctely !!! ");
                }
            }
            while (!result);

            int NewStockQuantity = 0;
            do
            {
                Console.Write("- Enter the new Stock Quantity : ");
                result = int.TryParse(Console.ReadLine(), out int Quantity);
                if (result)
                {
                    NewStockQuantity = Quantity;
                }
                else
                {
                    Console.WriteLine("++ incorrect input format please Enter numbers !!! ");
                }
            }
            while (!result);
            // update product Stock Quantity

            await inventoryManagement.UpdateStockQuantityAsync(product_name, NewStockQuantity);
        }

        //4- List all Products in a particular Category
        public async Task DisplayProductsByCategoryAsync()
        {
            await DisplayCategoriesAsync();
            bool result = true;
            int category_id = -1;

            do
            {
                Console.Write("- Enter Category number from (Categories List) : ");
                result = int.TryParse(Console.ReadLine(), out int id);
                if (result)
                {
                    category_id = id;
                }
                else
                {
                    Console.WriteLine("++ incorrect input format please Enter numbers !!! ");
                }
            }
            while (!result);

            await inventoryManagement.DisplayProducts_ByCategoryAsync(category_id - 1);

        }


        //5- List all Categories
        public async Task DisplayCategoriesAsync()
        {
            Console.WriteLine($" Categories List : ");
            await inventoryManagement.DisplayAllCategoriesAsync();
        }


        //6- Calculate and display the total inventory value 
        public async Task DisplayTotalInventoryAsync()
        {
            //get total inventory value
            await inventoryManagement.DisplayTotalInventoryAsync();
        }

       
    }
}
