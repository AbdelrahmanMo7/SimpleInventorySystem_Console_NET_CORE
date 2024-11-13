using SimpleInventorySystem.DataLayer.Models;
using SimpleInventorySystem.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleInventorySystem.BusinessLogic
{
    public class InventoryManagement : IInventoryManagement
    {

        private readonly IProductRepository productRepository;

        private readonly ICategoryRepository categoryRepository;

        public InventoryManagement(IProductRepository _productRepository, ICategoryRepository _categoryRepository)
        {
            this.productRepository = _productRepository;
            this.categoryRepository = _categoryRepository;

        }

        // display all products in a particular category.
        public async Task DisplayProducts_ByCategoryAsync(int category_index)
        {

            try
            {
                // first check if the selected category is already exists
                var Categories =await this.categoryRepository.GetAllAsync()  ;
                Category selectedCategory =(Categories.ToList())[category_index];

                if (selectedCategory != null)
                {
                   
                    // then get the category product list from database
                    IEnumerable<Product> Category_Products_List = await this.productRepository.GetByCategoryAsync(selectedCategory.Id);
                    Console.WriteLine($"-- Products list of : {selectedCategory.Category_Name} Category : ");

                    // didplay the list to the user
                    foreach (Product product in Category_Products_List.ToList())
                    {
                        Console.WriteLine(product);
                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //Calculate and display the total inventory value (sum of the Price *StockQuantity for all products).
        public async Task DisplayTotalInventoryAsync()
        {
            try
            {
                IEnumerable<Product> AllProducts= await this.productRepository.GetAllAsync();
                
                //Using LINQ to to calculate the sum of all inventery valus.
                double TotalInventoryValue =AllProducts.Sum(x => x.StockQuantity * x.Price);

                Console.WriteLine($"** Total Inventory Value is : {TotalInventoryValue:C} **");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // add new product so the function will check if this product exists :
        // if the result is false then the product will be added and if true the function will throw an exception to be handled
        public async Task AddNewProductAsync(Product product)
        {
            try
            {
                
                //check if this product exists
                Product oldProduct = await this.productRepository.GetByNameAsync(product.Product_Name);
               

                if (oldProduct == null)
                {
                    // first check if the selected category is already exists
                    var Categories = await this.categoryRepository.GetAllAsync();
                    Category selectedCategory = (Categories.ToList())[product.Category_ID];

                    if (selectedCategory != null)
                    {
                        // add new product
                        product.Category_ID = selectedCategory.Id;
                        await this.productRepository.AddAsync(product);
                        Console.WriteLine($"** Product named : {product.Product_Name} added successfully. **");

                    }

                }
                else
                {
                    throw new Exception($" The Product named : {product.Product_Name} is already exists. \r\n  -> You can use Update to increase the Stock Quantity...  ");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
        // Update existed product so the function will check if this product already exists :
        // if the result is true then the product will be updated and if false the function will throw an exception to be handled
        public async Task UpdateStockQuantityAsync(string product_name, int NewQuantity)
        {
            try
            {
                
                // check if this product already exists
                Product oldProduct = await this.productRepository.GetByNameAsync(product_name) ;

                if (oldProduct != null)
                {
                    // updating product Stock Quantity
                    oldProduct.StockQuantity = NewQuantity;
                    await this.productRepository.UpdateAsync(oldProduct);
                    Console.WriteLine($"** Stock Quantity of Product named : {product_name} updated successfully. **");

                }
                else
                {
                    throw new Exception($" The Product named : {product_name} is not exists. ");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //display all categories
        public async Task DisplayAllCategoriesAsync()
        {
            try
            {
                IEnumerable<Category> categories=await this.categoryRepository.GetAllAsync();
                for (int i=0; i<categories.ToList().Count;i++)
                {
                    Console.WriteLine($" {i+1} - {categories.ToList()[i]}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // add new category
        public async Task AddNewCategoryAsync(string category_name)
        {
            try
            {
               
                //check if this category exists

                var  oldcategory = await this.categoryRepository.GetByNameAsync(category_name);

                if (oldcategory == null)
                {
                    // add new category
                    await this.categoryRepository.AddAsync(new Category() { Category_Name=category_name});
                    Console.WriteLine($"** Category named : {category_name} added successfully. **");

                }
                else
                {
                    throw new Exception($" The Category named : {category_name} is already exists. \r\n  -> You can try again with another name....  ");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
